using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSpaceImplementation;
using OpenSpaceImplementation.AI;
using OpenSpaceImplementation.Animation;
using OpenSpaceImplementation.Animation.Component;
using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.Object;
using OpenSpaceImplementation.Object.Properties;
using OpenSpaceImplementation.Visual;
using OpenSpaceImplementation.Visual.Deform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace OpenSpaceImplementation.Animation {

    public class PersoAnimationBehaviour : MonoBehaviour {
        bool loaded = false;
        public Perso perso;
        public Controller controller;

        // States
        bool hasStates = false;
        public OpenSpaceImplementation.Object.Properties.State state = null;
        public string[] stateNames = { "Placeholder" };
        int currentState = 0;
        public int stateIndex = 0;
        public bool autoNextState = false;

        // Physical object lists
        public string[] poListNames = { "Null" };
        int currentPOList = 0;
        public int poListIndex = 0;

        // Animation
        public AnimA3DGeneral a3d = null;
        bool forceAnimUpdate = false;
        public uint currentFrame = 0;
        public bool playAnimation = true;
        public float animationSpeed = 15f;
        private float updateCounter = 0f;
        private PhysicalObject[][] subObjects = null; // [channel][ntto]
        private GameObject[] channelObjects = null;
        private int[] currentActivePO = null;
        private bool[] channelParents = null;
        public AnimMorphData[,] morphDataArray;
        private Dictionary<short, List<int>> channelIDDictionary = new Dictionary<short, List<int>>();
        private Dictionary<ushort, PhysicalObject>[] fullMorphPOs = null;
        bool hasBones = false; // We can optimize a tiny bit if this object doesn't have bones

        // Use this for initialization
        void Start()
        {
            perso = gameObject.GetComponent<Perso>();
            Init();
        }

        public void Init()
        {
            if (perso != null && perso.Perso3dData.Family != null) {
                Family fam = perso.Perso3dData.Family;
                if (fam != null && fam.objectLists != null && fam.objectLists.Count > 0) {
                    Array.Resize(ref poListNames, fam.objectLists.Count + 1);
                    Array.Copy(fam.objectLists.Select(o => (o == null ? "Null" : o.ToString())).ToArray(), 0, poListNames, 1, fam.objectLists.Count);

                    //Array.Copy(l.uncategorizedObjectLists.Select(o => (o == null ? "Null" : o.ToString())).ToArray(), 0, poListNames, 1 + fam.objectLists.Count, l.uncategorizedObjectLists.Count);

                    currentPOList = fam.GetIndexOfPhysicalList(perso.Perso3dData.ObjectList) + 1;
                    if (currentPOList == -1) currentPOList = 0;
                    poListIndex = currentPOList;
                }
                if (fam != null && fam.states != null && fam.states.Count > 0) {
                    stateNames = fam.states.Select(s => (s == null ? "Null" : s.ToString())).ToArray();
                    hasStates = true;
                    state = perso.Perso3dData.StateCurrent;
                    for (int i = 0; i < fam.states.Count; i++) {
                        if (state == fam.states[i]) {
                            currentState = i;
                            stateIndex = i;
                            SetState(i);
                            break;
                        }
                    }
                    if (state == null && fam.states.Count > 0) {
                        currentState = 0;
                        stateIndex = 0;
                        SetState(0);
                    }
                }
            }
            loaded = true;
        }

        public void SetState(int index)
        {
            if (index < 0 || index >= perso.Perso3dData.Family.states.Count) return;
            stateIndex = index;
            currentState = index;
            state = perso.Perso3dData.Family.states[index];
            //UpdateViewCollision(controller.viewCollision);

            // Set animation
            ushort anim_index = 0;
            byte bank_index = 0;
            if (state.anim_ref != null) {
                anim_index = state.anim_ref.anim_index;
                bank_index = perso.Perso3dData.Family.animBank;
            }

            if (state.anim_ref != null && state.anim_ref.a3d != null) {
                animationSpeed = state.speed;
                InitAnimation(state.anim_ref.a3d);
                UpdateAnimation();
            } else {
                a3d = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (loaded) {
                if (hasStates) {
                    if (stateIndex != currentState) {
                        currentState = stateIndex;
                        SetState(currentState);
                    }
                }

                if (poListIndex != currentPOList && perso.Perso3dData != null) {
                    if (poListIndex > 0 && poListIndex < perso.Perso3dData.Family.objectLists.Count + 1) {
                        currentPOList = poListIndex;
                        perso.Perso3dData.ObjectList = perso.Perso3dData.Family.objectLists[currentPOList - 1];
                    } else {
                        poListIndex = 0;
                        currentPOList = 0;
                        perso.Perso3dData.ObjectList = null;
                    }
                    forceAnimUpdate = true;
                    SetState(currentState);
                }
            }
            
            if (playAnimation) {
                updateCounter += Time.deltaTime * animationSpeed;
                // If the camera is not inside a sector, animations will only update 1 out of 2 times (w/ frameskip) to avoid lag
                uint passedFrames = (uint)Mathf.FloorToInt(updateCounter);
                updateCounter %= 1;
                currentFrame += passedFrames;
                if (a3d != null && currentFrame >= a3d.num_onlyFrames) {
                    if (autoNextState) {
                        AnimA3DGeneral prevAnim = a3d;
                        GotoAutoNextState();
                        if (a3d == prevAnim) {
                            currentFrame = currentFrame % a3d.num_onlyFrames;
                            UpdateAnimation();
                        }
                    } else {
                        currentFrame = currentFrame % a3d.num_onlyFrames;
                        UpdateAnimation();
                    }
                }  else {
                    UpdateAnimation();
                }
            }
        }

        void DeinitAnimation()
        {
            loaded = false;
            // Destroy currently loaded subobjects
            if (subObjects != null) {
                for (int i = 0; i < subObjects.Length; i++) {
                    if (subObjects[i] == null) continue;
                    for (int j = 0; j < subObjects[i].Length; j++) {
                        if (subObjects[i][j] != null) {
                            subObjects[i][j].Destroy();
                        }
                    }
                }
                subObjects = null;
            }
            if (channelObjects != null) {
                for (int i = 0; i < channelObjects.Length; i++) {
                    if (channelObjects[i] != null) {
                        Destroy(channelObjects[i]);
                    }
                }
                channelObjects = null;
            }
            if (fullMorphPOs != null) {
                for (int i = 0; i < fullMorphPOs.Length; i++) {
                    if (fullMorphPOs[i] != null) {
                        foreach (PhysicalObject po in fullMorphPOs[i].Values) {
                            po.Destroy();
                        }
                        fullMorphPOs[i].Clear();
                    }
                }
                fullMorphPOs = null;
            }
            channelIDDictionary.Clear();
            hasBones = false;
        }

        void InitAnimation(AnimA3DGeneral a3d)
        {
            if (a3d != this.a3d || forceAnimUpdate) {
                forceAnimUpdate = false;
                DeinitAnimation();
                // Init animation
                this.a3d = a3d;
                currentFrame = 0;
                if (a3d != null) {
                    //animationSpeed = a3d.speed;
                    // Init channels & subobjects
                    subObjects = new PhysicalObject[a3d.num_channels][];
                    channelObjects = new GameObject[a3d.num_channels];
                    if (a3d.num_morphData > 0) fullMorphPOs = new Dictionary<ushort, PhysicalObject>[a3d.num_channels];
                    currentActivePO = new int[a3d.num_channels];
                    channelParents = new bool[a3d.num_channels];
                    for (int i = 0; i < a3d.num_channels; i++) {
                        short id = a3d.channels[a3d.start_channels + i].id;
                        channelObjects[i] = new GameObject("Channel " + id);
                        channelObjects[i].transform.SetParent(perso.gameObject.transform);
                        currentActivePO[i] = -1;
                        AddChannelID(id, i);
                        subObjects[i] = new PhysicalObject[a3d.num_NTTO];
                        AnimChannel ch = a3d.channels[a3d.start_channels + i];
                        List<ushort> listOfNTTOforChannel = new List<ushort>();
                        for (int j = 0; j < a3d.num_onlyFrames; j++) {
                            AnimOnlyFrame of = a3d.onlyFrames[a3d.start_onlyFrames + j];
                            //print(ch.numOfNTTO + " - " + of.numOfNTTO + " - " + a3d.numOfNTTO.Length);
                            AnimNumOfNTTO numOfNTTO = a3d.numOfNTTO[ch.numOfNTTO + of.numOfNTTO];
                            if (!listOfNTTOforChannel.Contains(numOfNTTO.numOfNTTO)) {
                                listOfNTTOforChannel.Add(numOfNTTO.numOfNTTO);
                            }
                        }
                        for (int k = 0; k < listOfNTTOforChannel.Count; k++) {
                            int j = listOfNTTOforChannel[k] - a3d.start_NTTO;
                            AnimNTTO ntto = a3d.ntto[a3d.start_NTTO + j];
                            if (ntto.IsInvisibleNTTO) {
                                subObjects[i][j] = new PhysicalObject();
                                subObjects[i][j].Gao.transform.parent = channelObjects[i].transform;
                                subObjects[i][j].Gao.name = "[" + j + "] Invisible PO";
                                subObjects[i][j].Gao.SetActive(false);
                                /*GameObject boneVisualisation = new GameObject("Bone vis");
                                boneVisualisation.transform.SetParent(subObjects[i][j].Gao.transform);
                                MeshRenderer mr = boneVisualisation.AddComponent<MeshRenderer>();
                                MeshFilter mf = boneVisualisation.AddComponent<MeshFilter>();
                                Mesh mesh = Util.CreateBox(0.1f);
                                mf.mesh = mesh;
                                boneVisualisation.transform.localScale = Vector3.one / 4f;*/
                            } else {
                                if (perso.Perso3dData.ObjectList != null && perso.Perso3dData.ObjectList.Count > ntto.object_index) {
                                    PhysicalObject o = perso.Perso3dData.ObjectList.entries[ntto.object_index].po;
                                    if (o != null) {
                                        //if (o.visualSetType == 1) print(name);
                                        PhysicalObject c = o.Clone();
                                        subObjects[i][j] = c;
                                        subObjects[i][j].Gao.transform.localScale =
                                            subObjects[i][j].scaleMultiplier.HasValue ? subObjects[i][j].scaleMultiplier.Value : Vector3.one;
                                        c.Gao.transform.parent = channelObjects[i].transform;
                                        c.Gao.name = "[" + j + "] " + c.Gao.name;
                                        if (Controller.Settings.hasDeformations && c.Bones != null) {
                                            hasBones = true;
                                        }
                                        foreach (VisualSetLOD l in c.visualSet) {
                                            if (l.obj != null) {
                                                GameObject gao = l.obj.Gao; // TODO: Create gameobjects explicitly?
                                            }
                                        }

                                        c.Gao.SetActive(false);
                                    }
                                }
                            }
                        }
                    }

                    if (a3d.num_morphData > 0 && a3d.morphData != null && Controller.Settings.engineVersion < Settings.EngineVersion.R3) {
                        morphDataArray = new AnimMorphData[a3d.num_channels, a3d.num_onlyFrames];
                        // Iterate over morph data to find the correct channel and keyframe
                        for (int i = 0; i < a3d.num_morphData; i++) {
                            AnimMorphData m = a3d.morphData[a3d.start_morphData + i];
                            if (m != null) {
                                /*print("F:" + a3d.num_onlyFrames + " - C:" + a3d.num_channels + " - CF" + (a3d.num_onlyFrames * a3d.num_channels) + " - " +
                                    m.channel + " - " + m.frame + " - " + m.morphProgress + " - " + m.objectIndexTo + " - " + m.byte5 + " - " + m.byte6 + " - " + m.byte7);*/
                                int channelIndex = this.GetChannelByID(m.channel)[0];
                                if (channelIndex < morphDataArray.GetLength(0) && m.frame < morphDataArray.GetLength(1)) {
                                    morphDataArray[channelIndex, m.frame] = m;
                                    if (m.morphProgress == 100 && perso.Perso3dData.ObjectList != null && perso.Perso3dData.ObjectList.Count > m.objectIndexTo) {
                                        if (fullMorphPOs[channelIndex] == null) fullMorphPOs[channelIndex] = new Dictionary<ushort, PhysicalObject>();
                                        if (!fullMorphPOs[channelIndex].ContainsKey(m.objectIndexTo)) {
                                            PhysicalObject o = perso.Perso3dData.ObjectList.entries[m.objectIndexTo].po;
                                            if (o != null) {
                                                PhysicalObject c = o.Clone();
                                                c.Gao.transform.localScale = c.scaleMultiplier.HasValue ? c.scaleMultiplier.Value : Vector3.one;
                                                c.Gao.transform.parent = channelObjects[channelIndex].transform;
                                                c.Gao.name = "[Morph] " + c.Gao.name;
                                                if (Controller.Settings.hasDeformations && c.Bones != null) hasBones = true;
                                                foreach (VisualSetLOD l in c.visualSet) {
                                                    if (l.obj != null) {
                                                        GameObject gao = l.obj.Gao; // TODO: Create gameobjects explicitly?

                                                    }
                                                }
                                                
                                                c.Gao.SetActive(false);
                                                fullMorphPOs[channelIndex][m.objectIndexTo] = c;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } else {
                        morphDataArray = null;
                    }

                    // Keep lighting last so that it is applied to all new sub objects
                    /*if (!perso.IsAlways) {
                        Controller.SectorManager.ApplySectorLighting(sector, gameObject, LightInfo.ObjectLightedFlag.Perso);
                    } else {
                        Controller.SectorManager.ApplySectorLighting(sector, gameObject, LightInfo.ObjectLightedFlag.None);
                    }*/
                    // TODO: perso lighting
                }
                loaded = true;
            }
        }

        void UpdateAnimation()
        {
            if (loaded && a3d != null && channelObjects != null & subObjects != null) {
                if (currentFrame >= a3d.num_onlyFrames) currentFrame %= a3d.num_onlyFrames;
                // First pass: reset TRS for all sub objects
                for (int i = 0; i < channelParents.Length; i++) {
                    channelParents[i] = false;
                }
                AnimOnlyFrame of = a3d.onlyFrames[a3d.start_onlyFrames + currentFrame];
                // Create hierarchy for this frame
                for (int i = of.start_hierarchies_for_frame;
                    i < of.start_hierarchies_for_frame + of.num_hierarchies_for_frame; i++) {
                    AnimHierarchy h = a3d.hierarchies[i];

                    if (Controller.Settings.engineVersion <= Settings.EngineVersion.TT) {
                        channelObjects[h.childChannelID].transform.SetParent(channelObjects[h.parentChannelID].transform);
                        channelParents[h.childChannelID] = true;
                    } else {
                        if (!channelIDDictionary.ContainsKey(h.childChannelID) || !channelIDDictionary.ContainsKey(h.parentChannelID)) {
                            continue;
                        }
                        List<int> ch_child_list = GetChannelByID(h.childChannelID);
                        List<int> ch_parent_list = GetChannelByID(h.parentChannelID);
                        foreach (int ch_child in ch_child_list) {
                            foreach (int ch_parent in ch_parent_list) {
                                channelObjects[ch_child].transform.SetParent(channelObjects[ch_parent].transform);
                                channelParents[ch_child] = true;
                            }
                        }
                    }

                    //channelObjects[ch_child].transform.SetParent(channelObjects[ch_parent].transform);
                }
                // Final pass
                for (int i = 0; i < a3d.num_channels; i++) {
                    AnimChannel ch = a3d.channels[a3d.start_channels + i];
                    AnimFramesKFIndex kfi = a3d.framesKFIndex[currentFrame + ch.framesKF];
                    AnimKeyframe kf = a3d.keyframes[kfi.kf];
                    AnimVector pos = a3d.vectors[kf.positionVector];
                    AnimQuaternion qua = a3d.quaternions[kf.quaternion];
                    AnimVector scl = a3d.vectors[kf.scaleVector];
                    AnimNumOfNTTO numOfNTTO = a3d.numOfNTTO[ch.numOfNTTO + of.numOfNTTO];
                    AnimNTTO ntto = a3d.ntto[numOfNTTO.numOfNTTO];
                    //if (ntto.IsBoneNTTO) continue;
                    int poNum = numOfNTTO.numOfNTTO - a3d.start_NTTO;
                    PhysicalObject physicalObject = subObjects[i][poNum];
                    Vector3 vector = pos.vector;
                    Quaternion quaternion = qua.quaternion;
                    Vector3 scale = scl.vector;
                    int framesSinceKF = (int)currentFrame - (int)kf.frame;
                    AnimKeyframe nextKF = null;
                    int framesDifference;
                    float interpolation;
                    if (kf.IsEndKeyframe) {
                        AnimFramesKFIndex next_kfi = a3d.framesKFIndex[0 + ch.framesKF];
                        nextKF = a3d.keyframes[next_kfi.kf];
                        framesDifference = a3d.num_onlyFrames - 1 + (int)nextKF.frame - (int)kf.frame;
                        if (framesDifference == 0) {
                            interpolation = 0;
                        } else {
                            //interpolation = (float)(nextKF.interpolationFactor * (framesSinceKF / (float)framesDifference) + 1.0 * nextKF.interpolationFactor);
                            interpolation = framesSinceKF / (float)framesDifference;
                        }
                    } else {
                        nextKF = a3d.keyframes[kfi.kf + 1];
                        framesDifference = (int)nextKF.frame - (int)kf.frame;
                        //interpolation = (float)(nextKF.interpolationFactor * (framesSinceKF / (float)framesDifference) + 1.0 * nextKF.interpolationFactor);
                        interpolation = framesSinceKF / (float)framesDifference;
                    }
                    //print(interpolation);
                    //print(a3d.vectors.Length + " - " + nextKF.positionVector);
                    AnimVector pos2 = a3d.vectors[nextKF.positionVector];
                    AnimQuaternion qua2 = a3d.quaternions[nextKF.quaternion];
                    AnimVector scl2 = a3d.vectors[nextKF.scaleVector];
                    vector = Vector3.Lerp(pos.vector, pos2.vector, interpolation);
                    quaternion = Quaternion.Lerp(qua.quaternion, qua2.quaternion, interpolation);
                    scale = Vector3.Lerp(scl.vector, scl2.vector, interpolation);
                    float positionMultiplier = Mathf.Lerp(kf.positionMultiplier, nextKF.positionMultiplier, interpolation);

                    if (poNum != currentActivePO[i]) {
                        if (currentActivePO[i] == -2 && fullMorphPOs != null && fullMorphPOs[i] != null) {
                            foreach (PhysicalObject morphPO in fullMorphPOs[i].Values) {
                                if (morphPO.Gao.activeSelf) morphPO.Gao.SetActive(false);
                            }
                        }
                        if (currentActivePO[i] >= 0 && subObjects[i][currentActivePO[i]] != null) {
                            subObjects[i][currentActivePO[i]].Gao.SetActive(false);
                        }
                        currentActivePO[i] = poNum;
                        if (physicalObject != null) physicalObject.Gao.SetActive(true);
                    }
                    if (!channelParents[i]) channelObjects[i].transform.SetParent(perso.gameObject.transform);
                    channelObjects[i].transform.localPosition = vector * positionMultiplier;
                    channelObjects[i].transform.localRotation = quaternion;
                    channelObjects[i].transform.localScale = scale;

                    if (physicalObject != null && a3d.num_morphData > 0 && morphDataArray != null && i < morphDataArray.GetLength(0) && currentFrame < morphDataArray.GetLength(1)) {
                        AnimMorphData morphData = morphDataArray[i, currentFrame];

                        if (morphData != null && morphData.morphProgress != 0 && morphData.morphProgress != 100) {
                            PhysicalObject morphToPO = perso.Perso3dData.ObjectList.entries[morphData.objectIndexTo].po;
                            Vector3[] morphVerts = null;

                            for (int j = 0; j < physicalObject.visualSet.Length; j++) {
                                IGeometricObject obj = physicalObject.visualSet[j].obj;
                                if (obj == null || obj as MeshObject == null) continue;
                                MeshObject fromM = obj as MeshObject;
                                MeshObject toM = morphToPO.visualSet[j].obj as MeshObject;
                                if (toM == null) continue;
                                if (fromM.vertices.Length != toM.vertices.Length) {
                                    // For those special cases like the mistake in the Clark cinematic
                                    continue;
                                }
                                int numVertices = fromM.vertices.Length;
                                morphVerts = new Vector3[numVertices];
                                for (int vi = 0; vi < numVertices; vi++) {
                                    morphVerts[vi] = Vector3.Lerp(fromM.vertices[vi], toM.vertices[vi], morphData.morphProgressFloat);
                                }
                                for (int k = 0; k < fromM.num_subblocks; k++) {
                                    if (fromM.subblocks[k] == null || fromM.subblock_types[k] != 1) continue;
                                    MeshElement el = (MeshElement)fromM.subblocks[k];
                                    if (el != null) el.UpdateMeshVertices(morphVerts);
                                }
                            }
                        } else if (morphData != null && morphData.morphProgress == 100) {
                            physicalObject.Gao.SetActive(false);
                            PhysicalObject c = fullMorphPOs[i][morphData.objectIndexTo];
                            c.Gao.transform.localScale = c.scaleMultiplier.HasValue ? c.scaleMultiplier.Value : Vector3.one;
                            c.Gao.transform.localPosition = Vector3.zero;
                            c.Gao.transform.localRotation = Quaternion.identity;
                            c.Gao.SetActive(true);
                            currentActivePO[i] = -2;
                        } else {
                            for (int j = 0; j < physicalObject.visualSet.Length; j++) {
                                IGeometricObject obj = physicalObject.visualSet[j].obj;
                                if (obj == null || obj as MeshObject == null) continue;
                                MeshObject fromM = obj as MeshObject;
                                for (int k = 0; k < fromM.num_subblocks; k++) {
                                    if (fromM.subblocks[k] == null || fromM.subblock_types[k] != 1) continue;
                                    MeshElement el = (MeshElement)fromM.subblocks[k];
                                    if (el != null) el.ResetVertices();
                                }
                            }
                        }
                    }
                }
                if (hasBones) {
                    for (int i = 0; i < a3d.num_channels; i++) {
                        AnimChannel ch = a3d.channels[a3d.start_channels + i];
                        Transform baseChannelTransform = channelObjects[i].transform;
                        Vector3 invertedScale = new Vector3(1f / baseChannelTransform.localScale.x, 1f / baseChannelTransform.localScale.y, 1f / baseChannelTransform.localScale.z);
                        AnimNumOfNTTO numOfNTTO = a3d.numOfNTTO[ch.numOfNTTO + of.numOfNTTO];
                        AnimNTTO ntto = a3d.ntto[numOfNTTO.numOfNTTO];
                        PhysicalObject physicalObject = subObjects[i][numOfNTTO.numOfNTTO - a3d.start_NTTO];
                        if (physicalObject == null) continue;
                        DeformSet bones = physicalObject.Bones;
                        // Deformations
                        if (bones != null) {
                            for (int j = 0; j < a3d.num_deformations; j++) {
                                AnimDeformation d = a3d.deformations[a3d.start_deformations + j];
                                if (d.channel < ch.id) continue;
                                if (d.channel > ch.id) break;
                                if (!channelIDDictionary.ContainsKey(d.linkChannel)) continue;
                                List<int> ind_linkChannel_list = GetChannelByID(d.linkChannel);
                                foreach (int ind_linkChannel in ind_linkChannel_list) {
                                    AnimChannel ch_link = a3d.channels[a3d.start_channels + ind_linkChannel];
                                    AnimNumOfNTTO numOfNTTO_link = a3d.numOfNTTO[ch_link.numOfNTTO + of.numOfNTTO];
                                    AnimNTTO ntto_link = a3d.ntto[numOfNTTO_link.numOfNTTO];
                                    PhysicalObject physicalObject_link = subObjects[ind_linkChannel][numOfNTTO_link.numOfNTTO - a3d.start_NTTO];
                                    if (physicalObject_link == null) continue;
                                    if (bones == null || bones.bones.Length <= d.bone + 1) continue;
                                    DeformBone bone = bones.r3bones[d.bone + 1];
                                    if (bone != null) {
                                        Transform channelTransform = channelObjects[ind_linkChannel].transform;
                                        bone.UnityBone.transform.SetParent(channelTransform);
                                        bone.UnityBone.localPosition = Vector3.zero;
                                        bone.UnityBone.localRotation = Quaternion.identity;
                                        bone.UnityBone.localScale = Vector3.one;
                                        /*bone.UnityBone.position = channelTransform.position;
                                        bone.UnityBone.rotation = channelTransform.rotation;
                                        //bone.UnityBone.localScale = Vector3.one;
                                        bone.UnityBone.localScale = channelTransform.localScale;*/
                                    }
                                }
                            }
                        }
                    }
                }
                //this.currentFrame = (currentFrame + 1) % a3d.num_onlyFrames;
            }
        }

        List<int> GetChannelByID(short id)
        {
            if (channelIDDictionary.ContainsKey(id)) {
                return channelIDDictionary[id];
            } else return new List<int>();
        }

        void AddChannelID(short id, int index)
        {
            // Apparently there can be multiple channels with the ID -1, so this requires a list
            if (!channelIDDictionary.ContainsKey(id) || channelIDDictionary[id] == null) {
                channelIDDictionary[id] = new List<int>();
            }
            channelIDDictionary[id].Add(index);
        }

        void GotoAutoNextState()
        {
            if (state != null && state.NextStateAuto != null) {
                int indexOfStateAuto = perso.Perso3dData.Family.states.IndexOf(state.NextStateAuto);
                if (indexOfStateAuto > -1) SetState(indexOfStateAuto);
            }
        }
    }
}
