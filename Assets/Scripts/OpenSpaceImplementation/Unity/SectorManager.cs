using OpenSpaceImplementation;
using OpenSpaceImplementation.LevelLoading;
using OpenSpaceImplementation.Sectors;
using OpenSpaceImplementation.Unity;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class SectorManager : MonoBehaviour {
    public bool displayInactiveSectors = true; bool _displayInactiveSectors = true;

    public List<Sector> Sectors
    {
        get
        {
            if (SectorParentGao != null) {
                return SectorParentGao.GetComponentsInChildren<Sector>().ToList();
            }
            return new List<Sector>();
        }
    }
    //private List<SectorComponent> sectorComponents;
    public Camera mainCamera;
    //public List<Sector> activeSectors = new List<Sector>();
    public Sector activeSector = null;
    Vector3 camPosPrevious;

    public const int MaxLightCount = 500; // Used to size arrays that go into light shaders :)

    public GameObject SectorParentGao;

    public void LoadSectors(GameObject sectorRoot, Dictionary<String, SerializedWorldData.ESector> eSectors)
    {
        SectorParentGao = sectorRoot;

        foreach (var sectorEntry in eSectors) {

            GameObject sectorGao = new GameObject("Sector " + sectorEntry.Key);
            sectorGao.transform.parent = sectorRoot.transform;

            SerializedWorldData.ESector eSector = sectorEntry.Value;
            Sector sector = sectorGao.AddComponent<Sector>();
            sector.offsetString = sectorEntry.Key;
            sector.Visuals = eSector.Geometry.Values.Select((g) => g.Visuals).ToList();
            sector.Collision = eSector.Geometry.Values.Select((g) => g.Collision).ToList();

            foreach(string lightReference in eSector.LightReferences) {
                LightBehaviour light = Controller.LightManager.GetLight(lightReference);
                if (light != null) {
                    light.Sectors.Add(sector); // Link lights to sector
                }
            }

            sector.CreateGameObjects();
        }

        // Fill neighbour list
        foreach (var sectorEntry in eSectors) {

            Sector sector = GetSectorFromOffset(sectorEntry.Key);
            SerializedWorldData.ESector eSector = sectorEntry.Value;
            foreach (string neighbour in eSector.Neighbours) {
                sector.Neighbours.Add(GetSectorFromOffset(neighbour));
            }
        }
    }

    public Sector GetSectorFromOffset(string offset)
    {
        return Sectors.Find(s => s.offsetString == offset);
    }

    public void Clear()
    {
        this.Sectors.Clear();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Camera.main == null) {
            return;
        }
        Vector3 camPos = Camera.main.transform.localPosition;
        //activeSector = GetActiveSectorAtPoint(camPos, activeSector);

        if (_displayInactiveSectors != displayInactiveSectors) {
            _displayInactiveSectors = displayInactiveSectors;
            UpdateSectors();
        } else if (camPos != camPosPrevious) {
            camPosPrevious = camPos;
            UpdateSectors();
        }
    }

    private void UpdateSectors()
    {
        /*if (!displayInactiveSectors) {
			for (int i = 0; i < sectors.Count; i++) {
				Sector s = sectors[i];
				if (s.Loaded) {
					s.Gao.SetActive(true);
				} else {
					s.Gao.SetActive(false);
				}
			}
		} else {
			for (int i = 0; i < sectors.Count; i++) {
				Sector s = sectors[i];
				s.Gao.SetActive(true);
			}
		}*/
        // TODO: sector loading and unloading
    }

    /*public Sector GetActiveSectorAtPoint(Vector3 point, Sector currentActiveSector = null, bool allowVirtual = false) {

        if (currentActiveSector!=null && currentActiveSector.sectorBorder.ContainsPoint(point)) {
            return currentActiveSector;
        }

        Sector activeSector = null;
        for (int i = 0; i < sectors.Count; i++) {
            Sector s = sectors[i];
            s.Loaded = false;
        }
        for (int i = 0; i < sectors.Count; i++) {
            Sector s = sectors[i];
            s.Active = (allowVirtual || s.isSectorVirtual == 0) && (s.sectorBorder != null ? s.sectorBorder.ContainsPoint(point) : true);
            if (s.Active) {
                activeSector = s;

                for (int j = 0; j < s.neighbors.Count; j++) {
                    s.neighbors[j].sector.Loaded = true;
                }

                break;
            }
        }
        if (activeSector == null) {
            for (int i = 0; i < sectors.Count; i++) {
                sectors[i].Loaded = true;
            }
        } else {
            activeSector.Loaded = true;
        }

        return activeSector;
    }*/

    public void Init()
    {
        //sectors = MapLoader.Loader.sectors;
        /*sectorComponents = sectors.Select(s => s.Gao.AddComponent<SectorComponent>()).ToList();
        for (int i = 0; i < sectors.Count; i++) {
            Sector s = sectors[i];
            sectorComponents[i].sector = s;
            sectorComponents[i].Init();
            ApplySectorLighting(s, s.Gao, LightInfo.ObjectLightedFlag.Environment);
        }*/

    }

    public void RecalculateSectorLighting()
    {

        foreach (Sector s in Sectors) {
            ApplySectorLighting(s, s.gameObject, LightInfo.ObjectLightedFlag.Environment);
            /*foreach (Perso p in s.persos) {
                if (p.Gao) {
                    PersoBehaviour pb = p.Gao.GetComponent<PersoBehaviour>();
                    if (pb != null) {
                        pb.sector = s;
                    }
                }
            }*/
        }

        /* TODO: calculate perso lighting
            foreach (Perso p in MapLoader.Loader.persos) {
            PersoBehaviour pb = p.Gao.GetComponent<PersoBehaviour>();
            ApplySectorLighting(pb.sector, p.Gao, LightInfo.ObjectLightedFlag.Perso);
        }*/
    }

    public void ApplySectorLighting(Sector s, GameObject gao, LightInfo.ObjectLightedFlag objectType)
    {
        if (objectType == LightInfo.ObjectLightedFlag.None) {
            if (gao) {
                List<Renderer> rs = gao.GetComponents<Renderer>().ToList();
                foreach (Renderer r in rs) {
                    if (r.sharedMaterial.shader.name.Contains("Gouraud") || r.sharedMaterial.shader.name.Contains("Texture Blend")) {
                        r.sharedMaterial.SetFloat("_DisableLightingLocal", 1);
                    }
                }
                rs = gao.GetComponentsInChildren<Renderer>(true).ToList();
                foreach (Renderer r in rs) {
                    if (r.sharedMaterial.shader.name.Contains("Gouraud") || r.sharedMaterial.shader.name.Contains("Texture Blend")) {
                        r.sharedMaterial.SetFloat("_DisableLightingLocal", 1);
                    }
                }
            }
        } else {
            if (s == null) return;
            if (s.Lights != null) {
                Vector4? fogColor = null;
                Vector4? fogParams = null;
                Vector4 ambientLight = Vector4.zero;
                List<Vector4> staticLightPos = new List<Vector4>();
                List<Vector4> staticLightDir = new List<Vector4>();
                List<Vector4> staticLightCol = new List<Vector4>();
                List<Vector4> staticLightParams = new List<Vector4>();
                for (int i = 0; i < s.Lights.Count; i++) {
                    LightBehaviour light = s.Lights[i];

                    if (light == null) {
                        continue;
                    }

                    LightInfo lightInfo = s.Lights[i].lightInfo;
                    //if (!li.IsObjectLighted(objectType)) continue;
                    //if (li.turnedOn == 0x0) continue;
                    switch (lightInfo.type) {
                        case 4:
                            ambientLight += lightInfo.color;
                            staticLightPos.Add(new Vector4(light.transform.position.x, light.transform.position.y, light.transform.position.z, lightInfo.type));
                            staticLightDir.Add(light.transform.TransformVector(Vector3.back));
                            staticLightCol.Add(lightInfo.color);
                            staticLightParams.Add(new Vector4(lightInfo.near, lightInfo.far, lightInfo.paintingLightFlag, lightInfo.alphaLightFlag));
                            break;
                        case 6:
                            if (!fogColor.HasValue) {
                                fogColor = lightInfo.color;
                                fogParams = new Vector4(lightInfo.bigAlpha_fogBlendNear / 255f, lightInfo.intensityMin_fogBlendFar / 255f, lightInfo.near, lightInfo.far);
                            }
                            break;
                        default:
                            staticLightPos.Add(new Vector4(light.transform.position.x, light.transform.position.y, light.transform.position.z, lightInfo.type));
                            staticLightDir.Add(light.transform.TransformVector(Vector3.back));
                            staticLightCol.Add(lightInfo.color);
                            Vector3 scale = lightInfo.transMatrix.GetScale(true);
                            float maxScale = Mathf.Max(scale.x, scale.y, scale.z);
                            staticLightParams.Add(new Vector4(lightInfo.near * maxScale, lightInfo.far * maxScale, lightInfo.paintingLightFlag, lightInfo.alphaLightFlag));
                            break;
                    }
                }
                Vector4[] staticLightPosArray = staticLightPos.ToArray();
                Vector4[] staticLightDirArray = staticLightDir.ToArray();
                Vector4[] staticLightColArray = staticLightCol.ToArray();
                Vector4[] staticLightParamsArray = staticLightParams.ToArray();

                Array.Resize(ref staticLightPosArray, MaxLightCount);
                Array.Resize(ref staticLightDirArray, MaxLightCount);
                Array.Resize(ref staticLightColArray, MaxLightCount);
                Array.Resize(ref staticLightParamsArray, MaxLightCount);

                if (gao) {
                    List<Renderer> rs = gao.GetComponents<Renderer>().ToList();
                    foreach (Renderer r in rs) {
                        if (r.sharedMaterial.shader.name.Contains("Gouraud") || r.sharedMaterial.shader.name.Contains("Texture Blend")) {
                            MaterialPropertyBlock props = new MaterialPropertyBlock();
                            r.GetPropertyBlock(props);
                            if (fogColor.HasValue) props.SetVector("_SectorFog", fogColor.Value);
                            if (fogParams.HasValue) props.SetVector("_SectorFogParams", fogParams.Value);
                            //r.material.SetVector("_SectorAmbient", ambientLight);
                            props.SetFloat("_StaticLightCount", staticLightPosArray.Length);
                            if (staticLightPosArray.Length > 0) {
                                props.SetVectorArray("_StaticLightPos", staticLightPosArray);
                                props.SetVectorArray("_StaticLightDir", staticLightDirArray);
                                props.SetVectorArray("_StaticLightCol", staticLightColArray);
                                props.SetVectorArray("_StaticLightParams", staticLightParamsArray);
                            }
                            r.SetPropertyBlock(props);
                        }
                    }
                    rs = gao.GetComponentsInChildren<Renderer>(true).ToList();
                    foreach (Renderer r in rs) {
                        if (r.sharedMaterial != null &&
                            (r.sharedMaterial.shader.name.Contains("Gouraud") || r.sharedMaterial.shader.name.Contains("Texture Blend"))) {
                            MaterialPropertyBlock props = new MaterialPropertyBlock();
                            r.GetPropertyBlock(props);
                            if (fogColor.HasValue) props.SetVector("_SectorFog", fogColor.Value);
                            if (fogParams.HasValue) props.SetVector("_SectorFogParams", fogParams.Value);
                            //r.material.SetVector("_SectorAmbient", ambientLight);
                            props.SetFloat("_StaticLightCount", staticLightPosArray.Length);
                            if (staticLightPosArray.Length > 0) {
                                props.SetVectorArray("_StaticLightPos", staticLightPosArray);
                                props.SetVectorArray("_StaticLightDir", staticLightDirArray);
                                props.SetVectorArray("_StaticLightCol", staticLightColArray);
                                props.SetVectorArray("_StaticLightParams", staticLightParamsArray);
                            }
                            r.SetPropertyBlock(props);
                        }
                    }
                }
            }
        }
    }
}
