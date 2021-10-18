using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public IEnumerable<ISaveable> SaveableObjects; 

    // Start is called before the first frame update
    public void Start()
    {
        // Find all MonoBehaviours objects that implements ISaveable.
        SaveableObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
    }

    /// <summary>
    /// For each object (data managers) that implements the ISaveable 
    /// interface, call the save data method that is attached to them and 
    /// save it to a path specified in their class with an appropriate name.
    /// </summary>
    public void SaveGame()
    {
        if(SaveableObjects.Count() > 0)
        {
            foreach(ISaveable saveable in SaveableObjects)
            {
                saveable.SaveData();
                Debug.Log("Save: " + saveable.GetType() + " " + saveable.ToString());
            }
        }
        else
        {
            Debug.Log("ERROR: No saveable objects detected!");
        }
    }

    /// <summary>
    /// For each object (data managers) that implements the ISaveable 
    /// interface, the load data method that is attached to them and 
    /// load it straight onto the classes they are responsible for.
    /// </summary>
    public void LoadGame()
    {
        if(SaveableObjects.Count() > 0)
        {
            foreach(ISaveable saveable in SaveableObjects)
            {
                saveable.LoadData();

                Debug.Log("Load: " + saveable.GetType() + " " + saveable.ToString());
            }
        }
        else
        {
            Debug.Log("ERROR: No saveable objects found!");
        }
    }
}
