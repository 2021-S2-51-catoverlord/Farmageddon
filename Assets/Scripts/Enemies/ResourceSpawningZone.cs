using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawningZone : MonoBehaviour
{
    //markers for spawning zone
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;

    private float areaWidth; //relates to X value
    private float areaHeight; //relates to Y value

    //variables for spawning
    [SerializeField]
    private GameObject[] spawnList;
    [SerializeField]
    [Range(1, 100)]
    private int spawnRate;
    private int spawnListLength;

    [Range(1, 20)]
    public int MaxObjects;
    private int currentCount;

    void Start()
    {
        CalcSpawnArea();
        //this.gameObject.SetActive(false);
        spawnListLength = spawnList.Length - 1;
        MaxObjects = MaxObjects > 3 ? MaxObjects : 3;
        currentCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(1, 101) < spawnRate)
        {
            Instantiate(spawnList[Random.Range(0, spawnListLength)], GeneratePos(), Quaternion.identity);
            currentCount++;
        }

        // If quota is met
        if(currentCount >= MaxObjects)
        {
            this.gameObject.SetActive(false); // Stop spawning objects and set inactive.
        }
    }

    private void CalcSpawnArea()
    {
        float areaWidth = Mathf.Abs(pointA.localPosition.x) + Mathf.Abs(pointB.localPosition.x);
        this.areaWidth = areaWidth;
        float areaHeight = Mathf.Abs(pointA.localPosition.y) + Mathf.Abs(pointB.localPosition.y);
        this.areaHeight = areaHeight;
    }

    private Vector3 GeneratePos()
    {
        float newHeight = pointA.position.y - Random.Range(0, areaHeight);
        float newWidth = pointA.position.x + Random.Range(0, areaWidth);
        return new Vector3(newWidth, newHeight, -1);
    }
}
