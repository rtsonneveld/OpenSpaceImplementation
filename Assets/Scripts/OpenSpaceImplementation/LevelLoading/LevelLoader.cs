using Newtonsoft.Json;
using OpenSpaceImplementation.General;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OpenSpaceImplementation.LevelLoading {
    public class LevelLoader : MonoBehaviour {
        public string LevelName = "";
        public string LevelFolder = "Levels";

        public static KnownTypesBinder Binder = new KnownTypesBinder
        {
            KnownTypes = new List<Type> {
                typeof(System.String),
                typeof(UnityEngine.GameObject),
                typeof(UnityEngine.Vector2),
                typeof(UnityEngine.Vector2Int),
                typeof(UnityEngine.Vector3),
                typeof(UnityEngine.Vector3Int),
                typeof(UnityEngine.Vector4),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedGraphData),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedGraphData.EArc),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedGraphData.EGraph),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedGraphData.EGraphNode),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedGraphData.EWayPoint),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedPersoData),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedPersoData.EDsgVar),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedPersoData.EPerso),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedPersoData.EStandardGame),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedScene),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedWorldData),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedWorldData.EGeometry),
                typeof(OpenSpaceImplementation.LevelLoading.SerializedWorldData.ESector),
                typeof(OpenSpaceImplementation.LevelLoading.GameMaterialReference),
                typeof(OpenSpaceImplementation.LevelLoading.VisualMaterialReference),
                typeof(OpenSpaceImplementation.Visual.MeshObject),
                typeof(OpenSpaceImplementation.Visual.MeshElement),
                typeof(OpenSpaceImplementation.Visual.VisualMaterial),
                typeof(OpenSpaceImplementation.Visual.VisualMaterialTexture),
                typeof(OpenSpaceImplementation.Visual.TextureInfo),
                typeof(OpenSpaceImplementation.Collide.CollideMeshObject),
                typeof(OpenSpaceImplementation.Collide.CollideMeshElement),
                typeof(OpenSpaceImplementation.Collide.CollideSpheresElement),
                typeof(OpenSpaceImplementation.Collide.BoundingVolume),
            }
        };

        public static JsonSerializerSettings JsonImportSettings
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                settings.NullValueHandling = NullValueHandling.Include;
                settings.Converters.Add(new IgnoreUnityGameObjectsConverter());
                settings.Converters.Add(new GameMaterial.GameMaterialReferenceJsonConverter());
                settings.Converters.Add(new VisualMaterial.VisualMaterialReferenceJsonConverter());
                settings.SerializationBinder = Binder;

                return settings;
            }
        }

        public static JsonSerializerSettings JsonImportSettingsNoConverters
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                settings.NullValueHandling = NullValueHandling.Include;
                settings.SerializationBinder = Binder;

                return settings;
            }
        }

        public void LoadLevel()
        {
            Debug.Log("Load level "+LevelName);

            // Clear all resources before importing a level
            Controller.ControllerInstance.ClearAllResources();

            string levelPath = Path.Combine(Application.dataPath, Controller.ControllerInstance.ResourceFolder,LevelFolder,LevelName+".json");
            string levelJSON = File.ReadAllText(levelPath);
            SerializedScene level = JsonConvert.DeserializeObject<SerializedScene>(levelJSON, JsonImportSettings);

            Controller.ControllerInstance.DestroyWorldRoot();
            GameObject root = Controller.ControllerInstance.CreateWorldRoot(LevelName);

            GameObject sectorsRoot = new GameObject("Sectors");
            sectorsRoot.transform.parent = root.transform;
            
            Controller.SectorManager.LoadSectors(sectorsRoot, level.WorldData.Sectors);
        }
    }
}