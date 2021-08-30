public abstract class material : item
{
    private int value;
    public material(string itemName, int ID, string description, int value) : base(itemName, ID, description)
    {
        this.value = value;
    }
}
