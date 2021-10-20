using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MusicPlayerTest
{
    public GameObject MusicPlayer;
    public AudioSource Controller;
    public AudioListener Listener;
    public AudioClip Clip;

    [OneTimeSetUp]
    public void Setup()
    {
        MusicPlayer = new GameObject();
        Controller = new AudioSource();
        Listener = new AudioListener();
        Clip = Resources.Load<AudioClip>("Music");
    }

    [Test]
    public void TestCheckAudioListeners_FailCase()
    {
        MusicPlayer.AddComponent<AudioListener>();

        // Here, an extra Audio Listener is added to the GameObject, rule 
        // is that there can only be one Audio Listener in a scene.
        Assert.False(MusicPlayer.AddComponent<AudioListener>());
    }

    [Test]
    public void TestCheckAudioListeners_PassCase()
    {
        MusicPlayer.AddComponent<AudioListener>();
        MusicPlayer.AddComponent<AudioListener>();

        // Music Player already has one Audio Listener hence the surplus components 
        // will not be added.
        Assert.IsTrue(MusicPlayer.GetComponents<AudioListener>().Length == 1);
    }

    [Test]
    public void TestLoadResources_FailCase()
    {
        // Resources.LoadAll returns an Object array. Data must be casted first before usage.
        Assert.IsAssignableFrom<AudioClip[]>(Resources.LoadAll("Music", typeof(AudioClip)));
    }

    [Test]
    public void TestLoadResources_PassCase()
    {
        // Ideal way of selecting data structure for loaded data (to use an Object array to store loaded dataa).
        Assert.IsAssignableFrom<Object[]>(Resources.LoadAll("Music", typeof(AudioClip)));
    }

    [Test]
    public void TestAudioContinuity()
    {
        // Will not be able to play music in Editor mode.
        MusicPlayer.AddComponent<AudioSource>().loop = true;
        MusicPlayer.GetComponent<AudioSource>().Play();
        Assert.IsTrue(MusicPlayer.GetComponent<AudioSource>().loop);
    }
}
