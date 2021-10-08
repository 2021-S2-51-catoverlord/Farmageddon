using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Utility class to read byte-data/write objects from/to a file specified by the caller. 
/// </summary>
public static class FileReadWrite
{
    /// <summary>
    /// Method to convert passed-in object to bytes and write it to 
    /// a file specified by the caller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <param name="objectToWrite"></param>
    public static void WriteBinToFile<T>(string fileName, T objectToWrite)
    {
        using Stream outStream = File.Open(fileName, FileMode.Create); 
        var binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(outStream, objectToWrite);
    }

    /// <summary>
    /// Method to read byte data from a file and deserialize it and 
    /// return the reconstructed object back to the caller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <returns>Deserialized object</returns>
    public static T ReadBinFromFile<T>(string filePath)
    {
        using Stream inStream = File.Open(filePath, FileMode.Open);
        var binaryFormatter = new BinaryFormatter();
        return (T)binaryFormatter.Deserialize(inStream);
    }
}
