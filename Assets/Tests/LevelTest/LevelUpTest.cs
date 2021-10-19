using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelUpTest
{
    [UnityTest]
    public IEnumerator LevelIncreaseTest()
    {
        var temp = new GameObject().GetComponent<LevelSystem>();

        //temp.level = 1;
        //temp.experience = 10;
        //temp.experienceToNextLevel = 32;
        temp.TestConstruct(1, 10, 32);

        temp.GainEXP(30);

        bool result = temp.LevelledUp();

        Assert.IsTrue(result);

        yield return null;
    }
}
