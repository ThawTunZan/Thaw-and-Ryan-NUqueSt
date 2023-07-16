using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyAITest
{
    EnemyAI enemyAI;
    [SetUp]
    public void SetUp()
    {
        GameObject testGameObject = new GameObject();
        enemyAI = testGameObject.AddComponent<EnemyAI>();

    }
    // A Test behaves as an ordinary method
    [Test]
    public void FindRadiusTest()
    {
        double expectedRadius = 5;
        double actualRadius = enemyAI.FindRadius(4, 3);
        Assert.AreEqual(expectedRadius, actualRadius);
    }
}
