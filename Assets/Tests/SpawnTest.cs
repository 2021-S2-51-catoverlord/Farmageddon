using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class SpawnTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator checkCorrectSpawning()
    {

        var spawningZone = new GameObject().AddComponent<SpawningZone>();

        var enemyPrefab = Resources.Load("Assets/Prefabs/Entity/Enemy - grunt");

        GameObject pointA = new GameObject();
        GameObject pointB = new GameObject();
        pointA.transform.localPosition = new Vector3(-5, 5, 0); 
        pointB.transform.localPosition = new Vector3( 5,-5, 0);

        spawningZone.TestConstruct((GameObject)enemyPrefab, 100, pointA.transform, pointB.transform);
        spawningZone.TestSpawning();
        yield return 30;

        var enemy = GameObject.FindGameObjectWithTag("Enemy");
        var prefabOfEnemy = PrefabUtility.GetCorrespondingObjectFromOriginalSource(enemy);



        Assert.AreEqual(prefabOfEnemy, enemyPrefab);
    }
}
