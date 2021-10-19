using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelTest
{
    [UnityTest]
    public IEnumerator LevelUpTest()
    {
        var temp = new GameObject().AddComponent<LevelSystem>();

        temp.level = 1;
        temp.experience = 10;
        temp.experienceToNextLevel = 32;

        temp.GainEXP(30);

        yield return null;

        Assert.IsTrue(temp.LevelledUp());
    }
}
