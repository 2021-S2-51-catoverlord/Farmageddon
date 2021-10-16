using UnityEngine;

public class TreeController : MonoBehaviour
{
    public bool IsPlayerIn = false;
    public int Health = 3;

    public GameObject PreMT;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && IsPlayerIn == true)
        {
            Health--;

            if(Health == 0)
            {
                Instantiate(PreMT, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IsPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IsPlayerIn = false;
        }
    }
}
