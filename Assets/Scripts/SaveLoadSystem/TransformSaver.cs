using LitJson;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSaver : SaveableBehaviour
{
    // Store the keys we'll be including in the saved game as constants.
    private const string LOCAL_POSITION_KEY = "localPosition";
    private const string LOCAL_ROTATION_KEY = "localRotation";
    private const string LOCAL_SCALE_KEY = "localScale";

    /// <summary>
    /// Helper function that converts an object that Unity already 
    /// knows how to serialize (like Vector3, Quaternion, and others) 
    /// into a JsonData that can be included in the saved game.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private JsonData SerializeValue(object obj)
    {
        // Convert an object to JSON text, then parse this text back into a JSON
        // representation, but it means that we don't need to write the
        // (de)serialization code for built-in Unity types.
        return JsonMapper.ToObject(JsonUtility.ToJson(obj));
    }

    /// <summary>
    /// DeserializeValue works in reverse: given a JsonData, it produces
    /// a value of the desired type, as long as that type is one that
    /// Unity already knows how to serialize.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    private T DeserializeValue<T>(JsonData data)
    {
        return JsonUtility.FromJson<T>(data.ToJson());
    }

    /// <summary>
    /// Provides the saved data for this component.
    /// </summary>
    public override JsonData SavedData
    {
        get
        {
            var result = new JsonData();
            // Store the serialised position, rotation, and scale into result arr.
            result[LOCAL_POSITION_KEY] = SerializeValue(transform.localPosition);
            result[LOCAL_ROTATION_KEY] = SerializeValue(transform.localRotation);
            result[LOCAL_SCALE_KEY] = SerializeValue(transform.localScale);
            return result;
        }
    }

    /// <summary>
    /// Given some loaded data, updates the current state of the component.
    /// </summary>
    /// <param name="data"></param>
    public override void LoadFromData(JsonData data)
    {
        // Test to see if each item exists in the saved data.
        if (data.ContainsKey(LOCAL_POSITION_KEY))
        {
            // Update position
            transform.localPosition = DeserializeValue<Vector3>(data[LOCAL_POSITION_KEY]);
        }
        
        if (data.ContainsKey(LOCAL_ROTATION_KEY))
        {
            // Update rotation
            transform.localRotation = DeserializeValue<Quaternion>(data[LOCAL_ROTATION_KEY]);
        }
        
        if (data.ContainsKey(LOCAL_SCALE_KEY))
        {
            // Update scale
            transform.localScale = DeserializeValue<Vector3>(data[LOCAL_SCALE_KEY]);
        }
    }
}
