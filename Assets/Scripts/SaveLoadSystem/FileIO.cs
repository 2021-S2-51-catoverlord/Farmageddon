using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

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
            new BinaryFormatter().Serialize(outStream, objectToWrite);
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
        T loadedData = default;

        try
        {
            using Stream inStream = File.Open(fullPath, FileMode.Open);
            loadedData = (T)new BinaryFormatter().Deserialize(inStream);
        }
        catch(Exception e)
        {
            Debug.Log(e.StackTrace);
        }

        return loadedData;
    }

    /// <summary>
    /// Method to read byte data from a file, deserializes it 
    /// into a list-type data structure, and return the
    /// reconstructed objects back to the caller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fullPath"></param>
    /// <returns></returns>
    public static List<T> ReadListBinFromFile<T>(string fullPath)
    {
        List<T> loadedData = new List<T>();   
        try
        {
            using FileStream streamIn = File.OpenRead(fullPath);
            BinaryFormatter formatter = new BinaryFormatter();

            while(streamIn.Position != streamIn.Length)
            {
                loadedData.Add((T)new BinaryFormatter().Deserialize(streamIn));
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.StackTrace);
        }

        return loadedData;
    }
}
