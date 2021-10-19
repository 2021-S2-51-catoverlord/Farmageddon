using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningZone : MonoBehaviour
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




    void Start()
    {
        CalcSpawnArea();
<<<<<<< HEAD:Assets/Scripts/Enemies/ResourceSpawningZone.cs
<<<<<<< Updated upstream:Assets/Scripts/Enemies/ResourceSpawningZone.cs
        //this.gameObject.SetActive(false);
        spawnListLength = spawnList.Length - 1;
        MaxObjects = MaxObjects > 3 ? MaxObjects : 3;
        currentCount = 0;
=======
        this.gameObject.SetActive(false);
>>>>>>> Stashed changes:Assets/Scripts/Enemies/SpawningZone.cs
=======
        this.gameObject.SetActive(false);
        spawnListLength = spawnList.Length - 1;
>>>>>>> parent of ad7b8b5e (Made some Resouce Spawner Objects):Assets/Scripts/Enemies/SpawningZone.cs
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, 101) < spawnRate)
        {
<<<<<<< HEAD:Assets/Scripts/Enemies/ResourceSpawningZone.cs
<<<<<<< Updated upstream:Assets/Scripts/Enemies/ResourceSpawningZone.cs
            Instantiate(spawnList[Random.Range(0, spawnListLength)], GeneratePos(), Quaternion.identity);
            currentCount++;
        }

        // If quota is met
        if(currentCount >= MaxObjects)
        {
            this.gameObject.SetActive(false); // Stop spawning objects and set inactive.
=======
            Instantiate(spawnList[Random.Range(0, spawnList.Length)], GenerateEnemyPos(), Quaternion.identity);
>>>>>>> Stashed changes:Assets/Scripts/Enemies/SpawningZone.cs
=======
            Instantiate(spawnList[Random.Range(0, spawnListLength)], GenerateEnemyPos(), Quaternion.identity);
>>>>>>> parent of ad7b8b5e (Made some Resouce Spawner Objects):Assets/Scripts/Enemies/SpawningZone.cs
        }
    }

    private void CalcSpawnArea()
    {
        float areaWidth = Mathf.Abs(pointA.localPosition.x) + Mathf.Abs(pointB.localPosition.x);
        this.areaWidth = areaWidth;
        float areaHeight = Mathf.Abs(pointA.localPosition.y) + Mathf.Abs(pointB.localPosition.y);
        this.areaHeight = areaHeight;
    }

    private Vector3 GenerateEnemyPos()
    {
        float newHeight = pointA.position.y - Random.Range(0, areaHeight);
        float newWidth = pointA.position.x + Random.Range(0, areaWidth);
        return new Vector3(newWidth, newHeight, -1);
    }


}
