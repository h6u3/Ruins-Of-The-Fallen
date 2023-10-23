using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEngine.UI;

public class HungerTesting
{
    private PlayerManager playerManager;
    private PlayerStatsUI playerStatsUI;
    private PlayerStats playerStats;
    private Text testText;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = new GameObject();
        // Initialize the PlayerManager instance before each test
        playerManager = gameObject.AddComponent<PlayerManager>();
        playerStatsUI = gameObject.AddComponent<PlayerStatsUI>();
        playerStats = gameObject.AddComponent<PlayerStats>();
        // Creating Text components and attaching them to the PlayerStatsUI script
        testText = gameObject.AddComponent<Text>();
        playerStatsUI.HealthText = testText;
        playerStatsUI.HydrationText = testText;
        playerStatsUI.HungerText = testText;

        playerManager.playerStats = playerStats;
        playerManager.playerStats.player = playerStatsUI;
    }

    [UnityTest]
    public IEnumerator DecreaseHungerInUpdate()
    {
        //Update in playerManager should be running causing the hunger, hydration to decrease.
        // Arrange
        playerStatsUI.Health = 50;
        playerStatsUI.Hydration = 50;
        playerStatsUI.Hunger = 50;

        yield return new WaitForSeconds(10); 

        // Assert
        Assert.LessOrEqual(playerManager.playerStats.getHunger(), 49);
    }

    [UnityTest]
    public IEnumerator DecreaseHPAfterLowHungerInUpdate()
    {
        //Update in playerManager should be running causing the hunger, hydration to decrease.
        // Arrange
        playerStatsUI.Health = 1000;
        playerStatsUI.Hydration = 50;
        playerStatsUI.Hunger = 1;

        yield return new WaitForSeconds(20);

        // Assert
        Assert.LessOrEqual(playerManager.playerStats.getHunger(), 0);
        Assert.LessOrEqual(playerManager.playerStats.getHealth(), 999);
    }

}
