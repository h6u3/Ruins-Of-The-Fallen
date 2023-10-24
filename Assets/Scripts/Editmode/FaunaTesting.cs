using NUnit.Framework;
using System.IO;

public class FaunaTesting
{
    [Test]
    public void AnimalMoveSpeedTestingSimplePasses()
    {
        WanderAI wanderAI = new WanderAI();

        float expectedMoveSpeed = 3f;
        float actualMoveSpeed = wanderAI.moveSpeed;
        Assert.AreEqual(expectedMoveSpeed, actualMoveSpeed);
    }
}
