using LitJson;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    private const string ActiveSceneKey = "activeScene";
    private const string ScenesKey = "scenes";
    private const string ObjectsKey = "objects";
    private const string SaveIDKey = "$saveID";

    public static UnityAction<Scene, LoadSceneMode> LoadObjectsAfterSceneLoad;

    /// <summary>
    /// Saves saveral game objects as json, writes it to a file called 
    /// fileName in the game's persistent data directory.
    /// </summary>
    /// <param name="fileName"></param>
    public static void SaveGame(string fileName)
    {
        JsonData result = new JsonData();

        // Find all MonoBehaviours objects that implements ISaveableJson.
        IEnumerable<ISaveableJson> allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveableJson>();

        if(allSaveableObjects.Count() > 0)
        {
            JsonData savedObjects = new JsonData();

            // Iterate over every object we want to save
            foreach(ISaveableJson saveableObject in allSaveableObjects)
            {
                JsonData data = saveableObject.SavedData;

                if(data.IsObject)
                {
                    // Index this object with its Save ID
                    data[SaveIDKey] = saveableObject.SaveID;
                    savedObjects.Add(data);
                }
                else
                {
                    MonoBehaviour behaviour = saveableObject as MonoBehaviour;
                    Debug.LogWarningFormat(behaviour, "{0}'s save data is not a dictionary. The object was not saved.", behaviour.name);
                }
            }
            
            // Create an entry for objects saved.
            result[ObjectsKey] = savedObjects;
        }
        else
        {
            Debug.LogWarningFormat("The scene did not include any saveable objects.");
        }

        // Save scenes.

        JsonData openScenes = new JsonData();

        int sceneCount = SceneManager.sceneCount;


        // Save all scenes that are in the game.
        for(int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            openScenes.Add(scene.name);
        }

        result[ScenesKey] = openScenes;

        result[ActiveSceneKey] = SceneManager.GetActiveScene().name;

        string outputPath = Path.Combine(Application.persistentDataPath, fileName);

        JsonWriter writer = new JsonWriter();
        writer.PrettyPrint = true;

        result.ToJson(writer);

        File.WriteAllText(outputPath, writer.ToString());

        Debug.LogFormat("Wrote saved game to {0}", outputPath);

        result = null;
        System.GC.Collect();
    }

    /// <summary>
    /// Loads the game from a given file, and restores its state.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool LoadGame(string fileName)
    {
        // File location.
        string dataPath = Path.Combine(Application.persistentDataPath, fileName);

        if(!File.Exists(dataPath))
        {
            Debug.LogErrorFormat("No file exists at {0}", dataPath);
            return false;
        }

        // Read the data as JSON and map it into objects (deserialization).
        string text = File.ReadAllText(dataPath);
        JsonData data = JsonMapper.ToObject(text);

        // Ensure that we successfully read the data, and that it's an object.
        if(data == null || data.IsObject == false)
        {
            Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath);
            return false;
        }

        // We need to know what scenes to load.
        if(!data.ContainsKey("scenes"))
        {
            Debug.LogWarningFormat("Data at {0} does not contain any scenes; not loading any!", dataPath);
            return false;
        }

        // Get the list of scenes.
        JsonData scenes = data[ScenesKey];
        int sceneCount = scenes.Count;

        // If no scenes are saved...
        if(sceneCount == 0)
        {
            Debug.LogWarningFormat("Data at {0} doesn't specify any scenes to load.", dataPath);
            return false;
        }

        // Load each of the specified scene using the "dictionary".
        for(int i = 0; i < sceneCount; i++)
        {
            string scene = (string)scenes[i];

            if(i == 0)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Single); // Load it and replace every other active scenes.
            }
            else
            {                
                SceneManager.LoadScene(scene, LoadSceneMode.Additive); // Otherwise, load that scene on top of the existing ones.
            }
        }

        // Find the active scene, and set it as current displayed scene.
        if(data.ContainsKey(ActiveSceneKey))
        {
            string activeSceneName = (string)data[ActiveSceneKey];
            Scene activeScene = SceneManager.GetSceneByName(activeSceneName);

            if(!activeScene.IsValid()) // Check if the scene is a valid scene...
            {
                Debug.LogErrorFormat("Data at {0} specifies an active scene that doesn't exist. Stopping loading here.", dataPath);
                return false;
            }

            SceneManager.SetActiveScene(activeScene);
        }
        else
        {
            // This is not an error, since the first scene in the list will
            // be treated as active, but it's worth warning about.
            Debug.LogWarningFormat("Data at {0} does not specify an active scene.", dataPath);
        }

        // Find all objects in the scene and load them.
        if(data.ContainsKey(ObjectsKey))
        {
            JsonData objects = data[ObjectsKey];

            // We can't update the state of the objects right away because
            // Unity will not complete loading the scene until some time in
            // the future. Changes we made to the objects would revert
            // to how they're defined in the original scene. As a result, we
            // need to run the code after the scene manager reports that a
            // scene has finished loading.
            // To do this, we create a new delegate that contains our object-
            // loading code, and store that in LoadObjectsAfterSceneLoad.
            // This delegate is added to the SceneManager's sceneLoaded
            // event, which makes it run after the scene has finished loading.
            LoadObjectsAfterSceneLoad = (scene, loadSceneMode) =>
            {
                // Find all ISaveableJson objects, and build a dictionary that maps their Save IDs to the object (so that we can quickly look them up).
                var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveableJson>().ToDictionary(o => o.SaveID, o => o);

                // Get the collection of objects we need to load.
                var objectsCount = objects.Count;

                // For each item in the list...
                for(int i = 0; i < objectsCount; i++)
                {
                    // Get the saved data.
                    var objectData = objects[i];
                    // Get the Save ID from that data
                    var saveID = (string)objectData[SaveIDKey];

                    // Attempt to find the object in the scene(s) with that specific Save ID.
                    if(allLoadableObjects.ContainsKey(saveID))
                    {
                        var loadableObject = allLoadableObjects[saveID];
                        // Ask the object to load from this data.
                        loadableObject.LoadFromData(objectData);
                    }
                }

                // Remove this delegate from the sceneLoaded event so that it isn't called next time.
                SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
                // Release the reference to the delegate
                LoadObjectsAfterSceneLoad = null;
                // Tell the System to free up some memory.
                System.GC.Collect();
            };

            // Register the object-loading code to run after the scene loads.
            SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
        }

        return true;
    }
}
