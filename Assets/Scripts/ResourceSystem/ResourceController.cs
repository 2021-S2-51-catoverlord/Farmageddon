using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    public int health = 3;

    [SerializeField]
    private GameObject resourceDropped;
    private ResourceField activeField;
    // getters & setters
    public ResourceField ActiveField { get => activeField; set => activeField = value; }
    private void Start()
    {
        activeField = GameObject.FindObjectOfType<ResourceField>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            health--;

            if (health == 0)
            {
                ResourceDrop();
            }
        }
    }

    private void ResourceDrop()
    {
        for (int i = 0; i < Random.Range(1,5); i++)
        {
            Instantiate(resourceDropped, transform.position, transform.rotation);
        }
        activeField.ResourceHarvested();
        Destroy(this.gameObject);
    }
}
