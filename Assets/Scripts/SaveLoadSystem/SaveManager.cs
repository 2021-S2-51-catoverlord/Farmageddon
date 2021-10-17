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
            Debug.Log("ERROR: No valid saves data found!");
        }
    }
}
