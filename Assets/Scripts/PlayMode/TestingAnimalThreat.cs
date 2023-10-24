using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Reflection;
public class TestingAnimalThreat
{

    public enum ThreatLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    
    [UnityTest]
    public IEnumerator TestingGetThreatLevel()
    {
        AnimalSpawner instance = new AnimalSpawner();

        MethodInfo methodInfo = typeof(AnimalSpawner).GetMethod("GetThreatLevel", BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo != null)
        {
            object result = methodInfo.Invoke(instance, null);
            ThreatLevel enumResult = (ThreatLevel)result;

            Assert.IsTrue(enumResult == ThreatLevel.Low || enumResult == ThreatLevel.Medium || enumResult == ThreatLevel.High);
        }

        yield break;
    }
}
