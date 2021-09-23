using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingManager : MonoBehaviour
{

    private Inventory playerInv;
    [SerializeField]
    private CraftSlot[] craftSlots;
    [SerializeField]
    private recipe[] recipeList;
    private void Start()
    {
        playerInv = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        
    }
    private void GenerateList()
    {

    }

    private void populateSlots()
    {

    }

}
[CreateAssetMenu(fileName ="New Recipe", menuName = "Crafting/Recipe")]
public class recipe
{
    private int id;
    private Item[][] requiredItem;
    private bool canCraft;
    public bool CanCraft { get => canCraft; set => canCraft = value; }
    public int ID { get => id;}

    public recipe(int ID, Item[][] requiredItem)
    {
        this.id = ID;
        this.requiredItem = requiredItem;
    }

}
