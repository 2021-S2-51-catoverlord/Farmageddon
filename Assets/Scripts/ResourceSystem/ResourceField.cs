using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceField : MonoBehaviour
{
    //markers for resource zone
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;

    private float areaWidth; //relates to X value
    private float areaHeight; //relates to Y value

    private DayNightCycleBehaviour worldClock;

    private int resourcesGathered = 0;
    [SerializeField]
    [Range(1, 100)]
    private int startPopulateAmount = 20;
    [SerializeField]
    private GameObject[] spawnList;
    private int spawnedResources = 0;
    [SerializeField]
    private int maxResources = 30;

    private void Start()
    {
        worldClock = Object.FindObjectOfType<DayNightCycleBehaviour>();
        CalcSpawnArea();
        PopulateField(startPopulateAmount);
    }

    private void Update()
    {
        if ( (worldClock.time == 1 || worldClock.time == 0) &&!(spawnedResources >= maxResources))
        {
            Debug.Log("canSpawn");
            PopulateField(resourcesGathered + Random.Range(0, 3));
            resourcesGathered = 0;
        }
    }

    private void PopulateField(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GenerateResourceNode();
            spawnedResources++;
            Debug.Log("new resrouce spawned, total resources spawned" + spawnedResources);
        }
    }

    private void GenerateResourceNode()
    {
        Instantiate(spawnList[Random.Range(0, spawnList.Length)], GenerateResourcePos(), Quaternion.identity); 
    }

    private void CalcSpawnArea()
    {
        float areaWidth = Mathf.Abs(pointA.localPosition.x) + Mathf.Abs(pointB.localPosition.x);
        this.areaWidth = areaWidth;
        float areaHeight = Mathf.Abs(pointA.localPosition.y) + Mathf.Abs(pointB.localPosition.y);
        this.areaHeight = areaHeight;
    }

    private Vector3 GenerateResourcePos()
    {
        float newHeight = pointA.position.y - Random.Range(0, areaHeight);
        float newWidth = pointA.position.x + Random.Range(0, areaWidth);
        return new Vector3(newWidth, newHeight, -1);
    }


    //count how many resources have been collected
    public void ResourceHarvested()
    {
        resourcesGathered++;
        spawnedResources--;
    }
}
