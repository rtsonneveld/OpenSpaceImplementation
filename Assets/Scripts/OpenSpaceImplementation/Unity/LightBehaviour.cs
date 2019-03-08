using OpenSpaceImplementation.Sectors;
using OpenSpaceImplementation.Visual;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OpenSpaceImplementation.Unity {

    public class LightBehaviour : MonoBehaviour {

        public string offsetString;
        public LightInfo lightInfo = new LightInfo();
        //public Light l = null;
        public Color color;
        public Color backgroundColor;
        float intensity;
        public float activeIntensity = 1f;
        public bool active = true;
        public LightManager lightManager;
        private Vector3 pos;
        private Quaternion rot;
        private Vector3 scl;
        private Color col;
        private Color bckCol;

        public List<Sector> Sectors = new List<Sector>();

        // Use this for initialization
        void Start()
        {
        }

        public void Init()
        {
            pos = transform.position;
            rot = transform.rotation;
            scl = transform.localScale;
            //color = new Color(Mathf.Clamp01(r3l.color.x), Mathf.Clamp01(r3l.color.y), Mathf.Clamp01(r3l.color.z), Mathf.Clamp01(r3l.color.w));
            intensity = Mathf.Max(lightInfo.color.x, lightInfo.color.y, lightInfo.color.z);
            if (intensity > 1) {
                Vector3 colorVector = new Vector3(lightInfo.color.x / intensity, lightInfo.color.y / intensity, lightInfo.color.z / intensity);
                color = new Color(Mathf.Clamp01(colorVector.x), Mathf.Clamp01(colorVector.y), Mathf.Clamp01(colorVector.z), Mathf.Clamp01(lightInfo.color.w));
            } else if (intensity > 0) {
                color = new Color(Mathf.Clamp01(lightInfo.color.x), Mathf.Clamp01(lightInfo.color.y), Mathf.Clamp01(lightInfo.color.z), Mathf.Clamp01(lightInfo.color.w));
            } else {
                // shadow, can't display it since colors are additive in Unity
            }
            backgroundColor = new Color(Mathf.Clamp01(lightInfo.background_color.x), Mathf.Clamp01(lightInfo.background_color.y), Mathf.Clamp01(lightInfo.background_color.z), Mathf.Clamp01(lightInfo.background_color.w));
            /*if (li.alphaLightFlag != 0) {
                color = new Color(color.r * li.color.w, color.g * li.color.w, color.b * li.color.w);
                backgroundColor = new Color(
                    backgroundColor.r * li.background_color.w,
                    backgroundColor.g * li.background_color.w,
                    backgroundColor.b * li.background_color.w);
            }*/
            col = color;
            bckCol = backgroundColor;
        }

        // Update is called once per frame
        void Update()
        {
            if (lightManager != null && false) {
                if (pos != transform.position || rot != transform.rotation || scl != transform.localScale || col != color || bckCol != backgroundColor) {
                    lightManager.sectorManager.RecalculateSectorLighting();
                    pos = transform.position;
                    rot = transform.rotation;
                    scl = transform.localScale;
                    if (Controller.Settings.engineVersion == Settings.EngineVersion.R3) {
                        lightInfo.transMatrix.type = 7;
                        lightInfo.transMatrix.SetTRS(transform.position, transform.rotation, transform.localScale, convertAxes: true, setVec: true);
                    } else {
                        lightInfo.transMatrix.SetTRS(transform.position, transform.rotation, transform.localScale, convertAxes: true, setVec: false);
                    }
                    intensity = Mathf.Max(lightInfo.color.x, lightInfo.color.y, lightInfo.color.z);
                    lightInfo.color = color;
                    lightInfo.background_color = backgroundColor;
                    if (intensity > 1) {
                        Vector3 colorVector = new Vector3(lightInfo.color.x / intensity, lightInfo.color.y / intensity, lightInfo.color.z / intensity);
                        color = new Color(Mathf.Clamp01(colorVector.x), Mathf.Clamp01(colorVector.y), Mathf.Clamp01(colorVector.z), Mathf.Clamp01(lightInfo.color.w));
                    } else if (intensity > 0) {
                        color = new Color(Mathf.Clamp01(lightInfo.color.x), Mathf.Clamp01(lightInfo.color.y), Mathf.Clamp01(lightInfo.color.z), Mathf.Clamp01(lightInfo.color.w));
                    } else {
                        // shadow, can't display it since colors are additive in Unity
                    }
                    backgroundColor = new Color(Mathf.Clamp01(lightInfo.background_color.x), Mathf.Clamp01(lightInfo.background_color.y), Mathf.Clamp01(lightInfo.background_color.z), Mathf.Clamp01(lightInfo.background_color.w));
                    bckCol = backgroundColor;
                    col = color;
                }
            }
        }

        public void Activate()
        {
            active = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            active = false;
            //gameObject.SetActive(false);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = new Color(color.r, color.g, color.b, 1f);
            if (lightInfo == null) {
                return;
            }
            switch (lightInfo.type) {
                case 2:
                case 7:
                case 8:
                    Gizmos.DrawIcon(transform.position, "PointLight Gizmo", true);
                    break;
                case 1:
                    Gizmos.DrawIcon(transform.position, "DirectionalLight Gizmo", true);
                    break;
                case 4:
                    Gizmos.DrawIcon(transform.position, "AreaLight Gizmo", true);
                    break;
            }
        }
        public void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(color.r, color.g, color.b, 1f);
            Gizmos.matrix = Matrix4x4.identity;

            if (lightInfo == null) {
                return;
            }

            switch (lightInfo.type) {
                case 1:
                    Gizmos.DrawRay(transform.position, transform.rotation.eulerAngles); break;
                case 2:
                case 7:
                case 8:
                    Gizmos.DrawWireSphere(transform.position, lightInfo.far); break;
            }
        }
    }
}