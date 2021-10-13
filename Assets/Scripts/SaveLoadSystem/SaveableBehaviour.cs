using LitJson;
using UnityEngine;

public abstract class SaveableBehaviour : MonoBehaviour, ISaveableJson, ISerializationCallbackReceiver
{
    [HideInInspector]
    [SerializeField]
    private string _saveID; // Stores a unique identifier for the object (non-visible).

    // Getter and setter for SaveID
    public string SaveID
    {
        get
        {
            return _saveID;
        }
        set
        {
            _saveID = value;
        }
    }

    // Abstract attributes/methods from ISaveableJson, subclasses must have their own implementation.
    public abstract JsonData SavedData { get; }
    public abstract void LoadFromData(JsonData data);

    /// <summary>
    /// OnBeforeSerialize is called when Unity is about 
    /// to save this object as part of a scene file
    /// </summary>
    public void OnBeforeSerialize()
    {
        if(_saveID == null)
        {
            // Initialise SaveID with a GUID.
            _saveID = System.Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// OnAfterDeserialize is called when Unity has 
    /// loaded this object as part of a scene file.
    /// </summary>
    public void OnAfterDeserialize()
    {
        // No implementation for after deserialisation. This is inherited from ISerializationCallbackReceiver.
    }
}

