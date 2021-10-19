using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class SpawnTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator InBoundsTest()
    {

        var spawningZone = new GameObject().AddComponent<SpawningZone>();

        

        GameObject pointA = new GameObject();
        GameObject pointB = new GameObject();
        pointA.transform.localPosition = new Vector3(-5, 5, 0); 
        pointB.transform.localPosition = new Vector3( 5,-5, 0);

        spawningZone.TestConstruct(pointA.transform, pointB.transform);

        Vector3 Spawnloc = spawningZone.TestBounds();

        yield return null;

        



        Assert.True(pointA.transform.localPosition.x < Spawnloc.x && Spawnloc.x < pointB.transform.localPosition.x && pointB.transform.localPosition.y < Spawnloc.y && Spawnloc.y < pointA.transform.localPosition.y);
    }
}
