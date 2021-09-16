using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{
    private Vector2[] cropCoords = new Vector2[70];
    public GameObject cropHolder;

    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        for(float i = -4.5f; i <= 8.5f; i++)
        {
            for(float g = 12.5f; g >= 8.5f; g -= 1)
            {
                Instantiate(cropHolder, new Vector2(i, g), Quaternion.identity);
                cropCoords[x] = new Vector2(i, g);
                x++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
