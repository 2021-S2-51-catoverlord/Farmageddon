using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropControl : MonoBehaviour
{
    public Sprite noCropObj;
    public Sprite carrot;
    public Sprite carrot2;
    public Sprite carrot3;
    public Sprite carrot4;
    public Sprite carrot5;
    public Sprite carrot6;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Click on crop");
       // if (//script of tool == tool name;)
        {
            //Destroy(gameObject);
            GetComponent<SpriteRenderer>().sprite = noCropObj;
        }
    }






}
