using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CheckPointTesting
{
    [Test]
    public void AnimalMoveSpeedTestingSimplePasses()
    {
        WanderAI wanderAI = new WanderAI();

        float expectedMoveSpeed = 3f;
        float actualMoveSpeed = wanderAI.moveSpeed;
        Assert.AreEqual( expectedMoveSpeed, actualMoveSpeed );
    }

}
