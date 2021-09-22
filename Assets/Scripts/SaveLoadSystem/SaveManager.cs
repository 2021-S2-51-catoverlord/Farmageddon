using LitJson;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    // Frequently used strings.
    private const string ACTIVE_SCENE_KEY = "activeScene";
    private const string SCENES_KEY = "scenes";
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID"; // "$" is used to reduce the chance of a naming conflict.

    // A reference to the delegate that runs after the scene loads, which performs the object state restoration.
    public static UnityEngine.Events.UnityAction<Scene, LoadSceneMode> LoadObjectsAfterSceneLoad;

    /// <summary>
    /// Saves the game, and writes it to a file called fileName in the game's
    /// persistent data directory.
    /// </summary>
    /// <param name="fileName"></param>
    public static void SaveGame(string fileName)
    {
        // Create the JsonData that we will eventually write to disk
        var result = new JsonData();

        // Find all MonoBehaviours by first finding every MonoBehaviour,
        // and filtering it (only include those that are ISaveable).
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        // If there are objects to save...
        if(allSaveableObjects.Count() > 0)
        {
            // Create the JsonData that will store the list of objects
            var savedObjects = new JsonData();

            // Iterate over every object we want to save
            foreach(var saveableObject in allSaveableObjects)
            {
                // Get the object's saved data
                var data = saveableObject.SavedData;

                // We expect this to be an object (JSON's term for a
                // dictionary) because we need to include the object's SaveID.
                if(data.IsObject)
                {
                    // Record the Save ID for this object
                    data[SAVEID_KEY] = saveableObject.SaveID;
                    // Add the object's saved data to the collection.
                    savedObjects.Add(data);
                }
                else // Otherwise...
                {
                    // Provide a helpful warning.
                    var behaviour = saveableObject as MonoBehaviour;
                    Debug.LogWarningFormat(behaviour,"{0}'s save data is not a dictionary. The object was not saved.", behaviour.name);
                }
            }
            // Store the collection of saved objects in the result.
            result[OBJECTS_KEY] = savedObjects;
        }
        else
        {
            // Give a warning if there are no objects to save.
            Debug.LogWarningFormat("The scene did not include any saveable objects.");
        }

        // Next, we need to record what scenes are open. Unity lets you
        // have multiple scenes open at the same time, so we need to store
        // all of them, as well as which scene is the "active" scene (the
        // scene that new objects are added to, and which controls the
        // lighting settings for the game).

        // Create a JsonData that will store the list of open scenes.
        var openScenes = new JsonData();

        // Ask the scene manager how many scenes are open, and for each one, store the scene's name.
        var sceneCount = SceneManager.sceneCount;

        for(int i = 0; i < sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            openScenes.Add(scene.name);
        }

        // Store the list of open scenes.
        result[SCENES_KEY] = openScenes;

        // Store the name of the active scene.
        result[ACTIVE_SCENE_KEY] = SceneManager.GetActiveScene().name;

        // Output generated saved data to disk. 
        var outputPath = Path.Combine(Application.persistentDataPath, fileName);

        // Init a JsonWriter, the 'pretty-print' makes data easier to read and understand.
        var writer = new JsonWriter();
        writer.PrettyPrint = true;

        // Convert the saved data to JSON text.
        result.ToJson(writer);

        // Write the JSON text to disk.
        File.WriteAllText(outputPath, writer.ToString());

        // Notify where the saved game is.
        Debug.LogFormat("Wrote saved game to {0}", outputPath);
        
        // Tell the system to gc to free up some memory.
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
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);

        // If file does not exists...
        if(File.Exists(dataPath) == false)
        {
            // Notify user and return.
            Debug.LogErrorFormat("No file exists at {0}", dataPath);
            return false;
        }

        // Read the data as JSON and map it into objects (deserialization).
        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);

        // Ensure that we successfully read the data, and that it's an object.
        if(data == null || data.IsObject == false)
        {
            Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath);
            return false;
        }

        // We need to know what scenes to load.
        if(!data.ContainsKey("scenes"))
        {
            // Output warning if there is no scene key.
            Debug.LogWarningFormat("Data at {0} does not contain any scenes; not loading any!",dataPath);
            return false;
        }

        // Get the list of scenes.
        var scenes = data[SCENES_KEY];
        int sceneCount = scenes.Count;

        // If no scenes are saved...
        if(sceneCount == 0)
        {
            Debug.LogWarningFormat("Data at {0} doesn't specify any scenes to load.", dataPath);
            return false;
        }

        // Load each of the specified scene using the "dictionary".
        for (int i = 0; i < sceneCount; i++)
        {
            var scene = (string)scenes[i];

            // If this is the first scene we're loading...
            if(i == 0)
            {
                // Load it and replace every other active scenes.
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
            else
            {
                // Otherwise, load that scene on top of the existing ones.
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }

        // Find the active scene, and set it
        if(data.ContainsKey(ACTIVE_SCENE_KEY))
        {
            var activeSceneName = (string)data[ACTIVE_SCENE_KEY];
            var activeScene = SceneManager.GetSceneByName(activeSceneName);

            if(activeScene.IsValid() == false) // Check if the scene is a valid scene...
            {
                Debug.LogErrorFormat( "Data at {0} specifies an active scene that doesn't exist. Stopping loading here.", dataPath);
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
        if (data.ContainsKey(OBJECTS_KEY))
        {
            var objects = data[OBJECTS_KEY];

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
            LoadObjectsAfterSceneLoad = (scene, loadSceneMode) => {
                // Find all ISaveable objects, and build a dictionary that maps their Save IDs to the object (so that we can quickly look them up).
                var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o);

                // Get the collection of objects we need to load.
                var objectsCount = objects.Count;

                // For each item in the list...
                for(int i = 0; i < objectsCount; i++)
                {
                    // Get the saved data.
                    var objectData = objects[i];
                    // Get the Save ID from that data
                    var saveID = (string)objectData[SAVEID_KEY];

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
