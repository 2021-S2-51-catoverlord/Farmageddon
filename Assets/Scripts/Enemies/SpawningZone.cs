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
    private int spawnListLength;




    void Start()
    {
        CalcSpawnArea();
        this.gameObject.SetActive(false);
        spawnListLength = spawnList.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCheck())
        {
            Instantiate(spawnList[Random.Range(0, spawnListLength)], GenerateEnemyPos(), Quaternion.identity);
        }
    }

    private bool spawnCheck()
    {
        return Random.Range(1, 101) < spawnRate;
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
    public void TestConstruct(GameObject enemyPrefab, int spawnRate, Transform pointA, Transform pointB )
    {
        spawnList = new GameObject[1];
        spawnList[0] = enemyPrefab;
        this.spawnRate = spawnRate;
        this.pointA = pointA;
        this.pointB = pointB;
        this.gameObject.SetActive(true);
        CalcSpawnArea();
    }

}




















