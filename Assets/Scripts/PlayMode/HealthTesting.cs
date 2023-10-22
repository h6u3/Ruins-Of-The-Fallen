using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

//As a player I want to have an HP bar so that I can track my health

public class HealthTesting
{
    private PlayerStatsUI playerStatsUI;
    private Text testText;


    [SetUp]
    public void Setup()
    {
        // Creating a GameObject to attach PlayerStatsUI script and Text components
        GameObject gameObject = new GameObject();
        playerStatsUI = gameObject.AddComponent<PlayerStatsUI>();
        // Creating Text components and attaching them to the PlayerStatsUI script
        testText = gameObject.AddComponent<Text>();
        playerStatsUI.HealthText = testText;
        playerStatsUI.HydrationText = testText;
        playerStatsUI.HungerText = testText;
    }

    [UnityTest]
    public IEnumerator IncreaseHealthIncreasesHealthProperly()
    {
        // Arrange
        playerStatsUI.Health = 50;
        int value = 30;

        // Act
        playerStatsUI.IncreaseHealth(value);

        yield return new WaitForFixedUpdate();

        // Assert
        Assert.AreEqual(80, playerStatsUI.Health);
        Assert.AreEqual("HP: 80", testText.text);
    }

    [UnityTest]
    public IEnumerator DecreaseHealthDecreasesHealthProperly()
    {
        // Arrange
        playerStatsUI.Health = 70;
        int value = 20;

        // Act
        playerStatsUI.DecreaseHealth(value);

        yield return null;

        // Assert
        Assert.AreEqual(50, playerStatsUI.Health);
        Assert.AreEqual("HP: 50", testText.text);
    }

    [UnityTest]
    public IEnumerator IncreaseHydrationIncreasesHydrationProperly()
    {
        // Arrange
        playerStatsUI.Hydration = 40;
        int value = 20;

        // Act
        playerStatsUI.IncreaseHydration(value);

        yield return null;

        // Assert
        Assert.AreEqual(60, playerStatsUI.Hydration);
        Assert.AreEqual("Hydration: 60%", testText.text);
    }

    [UnityTest]
    public IEnumerator DecreaseHydrationDecreasesHydrationProperly()
    {
        // Arrange
        playerStatsUI.Hydration = 70;
        int value = 30;

        // Act
        playerStatsUI.DecreaseHydration(value);

        yield return null;

        // Assert
        Assert.AreEqual(40, playerStatsUI.Hydration);
        Assert.AreEqual("Hydration: 40%", testText.text);
    }

    [UnityTest]
    public IEnumerator IncreaseHungerIncreasesHungerProperly()
    {
        // Arrange
        playerStatsUI.Hunger = 25;
        int value = 15;

        // Act
        playerStatsUI.IncreaseHunger(value);

        yield return null;

        // Assert
        Assert.AreEqual(40, playerStatsUI.Hunger);
        Assert.AreEqual("Hunger: 40%", testText.text);
    }

    [UnityTest]
    public IEnumerator DecreaseHungerDecreasesHungerProperly()
    {
        // Arrange
        playerStatsUI.Hunger = 60;
        int value = 25;

        // Act
        playerStatsUI.DecreaseHunger(value);

        yield return null;

        // Assert
        Assert.AreEqual(35, playerStatsUI.Hunger);
        Assert.AreEqual("Hunger: 35", testText.text);
    }

}
