using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Utility class to handle file I/O when saving/loading game data. 
/// </summary>
public static class FileIO
{ 
    /// <summary>
    /// Method to convert passed-in object to bytes and write it to 
    /// a file specified by the caller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fullPath"></param>
    /// <param name="objectToWrite"></param>
    public static void WriteBinToFile<T>(string fullPath, T objectToWrite)
    {
        try
        {
            using Stream outStream = File.Open(fullPath, FileMode.Create);
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(outStream, objectToWrite);
        }
        catch(Exception e)
        {
            Debug.Log(e.StackTrace);
        }
    }

    /// <summary>
    /// Method to read byte data from a file and deserialize it and 
    /// return the reconstructed object back to the caller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fullPath"></param>
    /// <returns>Deserialized object</returns>
    public static T ReadBinFromFile<T>(string fullPath)
    {
        using Stream inStream = File.Open(fullPath, FileMode.Open);
        var binaryFormatter = new BinaryFormatter();
        return (T)binaryFormatter.Deserialize(inStream);
    }
}