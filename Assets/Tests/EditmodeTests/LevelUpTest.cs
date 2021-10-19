using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelUpTest
{
    [UnityTest]
    public IEnumerator LevelIncreaseTest()
    {
        var temp = new GameObject().AddComponent<LevelSystem>();

        //temp.level = 1;
        //temp.experience = 10;
        //temp.experienceToNextLevel = 32;
        temp.TestConstruct(1, 10, 32);

        temp.GainEXP(30);


        yield return null;

        Assert.IsTrue(temp.LevelledUp());
    }
}
