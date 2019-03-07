﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using OpenSpaceImplementation.Visual;
using OpenSpaceImplementation.Sectors;
using System;

namespace OpenSpaceImplementation.Unity {

    //[ExecuteAlways]
    public class LightManager : MonoBehaviour {

        public SectorManager sectorManager;
        //public MeshRenderer backgroundPanel; TODO: background panel
        private VisualMaterial backgroundMaterial;
        private Sector previousActiveBackgroundSector = null;
        private List<LightInfo> Lights = new List<LightInfo>();

        [Range(0.0f, 1.0f)]
        public float luminosity = 0.5f;
        private float _luminosity = 0.5f;

        public bool saturate = true;
        private bool _saturate = true;
        public bool useDefaultSettings = true;

        public bool enableLighting = true; private bool _enableLighting = true;
        public bool enableFog = true; private bool _enableFog = true;

        public GameObject LightParentGao;

        // Use this for initialization
        void Start()
        {

            if (useDefaultSettings) {
                luminosity = Controller.Settings.luminosity;
                saturate = Controller.Settings.saturate;
            }

            Shader.SetGlobalFloat("_Luminosity", luminosity);
            Shader.SetGlobalFloat("_Saturate", saturate ? 1f : 0f);

            // Set background UVs
            /*Mesh mesh = backgroundPanel.GetComponent<MeshFilter>().sharedMesh;
            List<Vector2> uvs = new List<Vector2>();
            mesh.GetUVs(0, uvs);
            mesh.SetUVs(0, uvs.Select(u => new Vector3(u.x, u.y, 1f)).ToList());*/
            /*backgroundScroll = backgroundPanel.gameObject.AddComponent<ScrollingTexture>();
            backgroundScroll.visMat = null;
            backgroundScroll.mat = null;*/
        }

        public LightInfo GetLight(string offset)
        {
            return Lights.Find(l => l.offsetString == offset);
        }

        // Update is called once per frame
        void Update()
        {
            if (_enableLighting != enableLighting) {
                _enableLighting = enableLighting;
                Shader.SetGlobalFloat("_DisableLighting", enableLighting ? 0f : 1f);
            }
            if (_enableFog != enableFog) {
                _enableFog = enableFog;
                Shader.SetGlobalFloat("_DisableFog", enableFog ? 0f : 1f);
            }
            if (_luminosity != luminosity) {
                _luminosity = luminosity;
                Shader.SetGlobalFloat("_Luminosity", luminosity);
            }
            if (_saturate != saturate) {
                _saturate = saturate;
                Shader.SetGlobalFloat("_Saturate", saturate ? 1f : 0f);
            }

            // Update background color or material
            Color? backgroundColor = null;
            VisualMaterial skyMaterial = null;
            Sector activeBackgroundSector = null;
            /*if (MapLoader.Loader.globals != null && MapLoader.Loader.globals.backgroundGameMaterial != null && MapLoader.Loader.globals.backgroundGameMaterial.visualMaterial != null) {
                skyMaterial = MapLoader.Loader.globals.backgroundGameMaterial.visualMaterial;
            } else {*/ 
                if (sectorManager != null && sectorManager.Sectors != null && sectorManager.Sectors.Count > 0) {
                    foreach (Sector s in sectorManager.Sectors.Values) {
                        if (!s.Active) continue;
                        if (s.skyMaterial != null && s.skyMaterial.textures.Count > 0 && s.skyMaterial.textures.Where(t => t.texture != null).Count() > 0) {
                            skyMaterial = s.skyMaterial;
                            activeBackgroundSector = s;
                            break;
                        } else {
                            foreach (LightInfo li in s.Lights) {
                                if (li.type == 6) {
                                    backgroundColor = li.background_color;
                                    break;
                                }
                            }
                        }
                    }
                }
            //} TODO: Implement global sky material
            /*if (skyMaterial != null) {
                backgroundPanel.gameObject.SetActive(true);
                if (backgroundMaterial != skyMaterial) {
                    backgroundMaterial = skyMaterial;
                    Material skyboxMat = skyMaterial.GetMaterial();
                    backgroundPanel.sharedMaterial = skyboxMat;
                }
                //skyboxMat.SetFloat("_DisableLighting", 1f);
                backgroundPanel.sharedMaterial.SetFloat("_DisableLightingLocal", 1f);
                if (activeBackgroundSector != null) {
                    if (activeBackgroundSector != previousActiveBackgroundSector) {
                        //backgroundPanel.material.SetFloat("_DisableLightingLocal", 0f);
                        sectorManager.ApplySectorLighting(activeBackgroundSector, backgroundPanel.gameObject, LightInfo.ObjectLightedFlag.Environment);
                        previousActiveBackgroundSector = activeBackgroundSector;
                    }
                } else {
                    //backgroundPanel.material.SetFloat("_DisableLighting", 1f);
                }
                //RenderSettings.skybox = skyboxMat;
                //Camera.main.clearFlags = CameraClearFlags.Skybox;
            } else {
                backgroundPanel.gameObject.SetActive(false);
                //RenderSettings.skybox = null;
                //Camera.main.clearFlags = CameraClearFlags.SolidColor;
            }
            if (backgroundColor.HasValue) {
                Camera.main.backgroundColor = backgroundColor.Value;
                //Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, backgroundColor.Value, 0.5f * Time.deltaTime);
            } else {
                Camera.main.backgroundColor = Color.black;
                //Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, 0.5f * Time.deltaTime);
            }*/
            /*if (useFog && Camera.main.renderingPath == RenderingPath.DeferredShading) {
                // Fog doesn't work for deferred
                Camera.main.renderingPath = RenderingPath.Forward;
            }
            bool fogSet = false;
            bool ambientSet = false;
            if (simulateSectorLoading) {
                List<LightInfo> ambientLights = new List<LightInfo>();
                List<LightInfo> fogLights = new List<LightInfo>();
                for (int i = 0; i < lights.Count; i++) {
                    LightInfo l = lights[i];
                    if (l.containingSectors.FirstOrDefault(s => (neighborSectorLights ? s.Loaded : s.Active)) != null) {
                        l.Light.Activate();
                        if (l.type == 6) fogLights.Add(l);
                        if (l.type == 4) ambientLights.Add(l);
                    } else {
                        l.Light.Deactivate();
                    }
                }
                if (useAmbientColor) {
                    Color ambientLight = Color.black;
                    if (ambientLights.Count > 0) {
                        LightInfo l = ambientLights[0];
                        ambientLight = l.color;
                    }
                    RenderSettings.ambientLight = ambientLight;
                }

                if (useFog) {
                    if (fogLights.Count > 0) {
                        LightInfo l = fogLights[0];
                        float minFogDist = Vector3.Distance(fogLights[0].Light.transform.position, Camera.main.transform.position);
                        for (int i = 1; i < fogLights.Count; i++) {
                            float fogDist = Vector3.Distance(fogLights[i].Light.transform.position, Camera.main.transform.position);
                            if (fogDist < l.far && fogDist < minFogDist) {
                                l = fogLights[i];
                            }
                        }
                        RenderSettings.fog = true;
                        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, l.Light.color, 0.5f * Time.deltaTime);
                        RenderSettings.fogMode = FogMode.Linear;
                        RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, l.near, 0.5f * Time.deltaTime);
                        RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, l.far, 0.5f * Time.deltaTime);
                        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, l.Light.backgroundColor, 0.5f * Time.deltaTime);
                        fogSet = true;
                    } else {
                        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, 0.5f * Time.deltaTime);
                        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, Color.black, 0.5f * Time.deltaTime);
                        RenderSettings.fogStartDistance += 50 * Time.deltaTime;
                        RenderSettings.fogEndDistance += 50 * Time.deltaTime;
                        if (RenderSettings.fogStartDistance > 500) {
                            RenderSettings.fog = false;
                        }
                    }
                }
            }
            else {
            */
                /*foreach(LightInfo l in Lights) {
                    l.Light.Activate();
                }*/
            //}
        }

        public void LoadLights(GameObject root, Dictionary<string, LightInfo> lights)
        {
            this.Lights = lights.Values.ToList();
            this.LightParentGao = root;

            foreach(var lightPair in lights) {
                LightInfo l = lightPair.Value;
                l.offsetString = lightPair.Key;
                l.CreateGameObject();
                Register(l);
            }

            if (useDefaultSettings) {
                luminosity = Controller.Settings.luminosity;
                saturate = Controller.Settings.saturate;
            }
        }

        public void Register(LightInfo light)
        {
            LightBehaviour l = light.Light;
            l.lightManager = this;
            l.Init();
            l.transform.parent = LightParentGao.transform;
        }

        public void RecalculateSectorLighting()
        {
            if (sectorManager != null) sectorManager.RecalculateSectorLighting();
        }
    }
}