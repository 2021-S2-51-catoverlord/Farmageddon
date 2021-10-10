using LitJson;

public interface ISaveable
{
    // Unique identifier that identifies a component in the save data. 
    // It's used for finding that object again when the game is loaded.
    public string SaveID { get; }

    // The SavedData is the content that will be written to disk.
    public JsonData SavedData { get; }

    // The object is provided with the data that was read, 
    // and is used to restore its previous state.
    public void LoadFromData(JsonData data);
}
