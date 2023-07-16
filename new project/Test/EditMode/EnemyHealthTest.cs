using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyHealthTest
{
    private EnemyHealth enemyHealth;
    private Animator animator;
    private PlayerQuests player;

    [SetUp]
    public void Setup()
    {
        GameObject enemyObject = new GameObject();
        enemyHealth = enemyObject.AddComponent<EnemyHealth>();
        animator = enemyObject.AddComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();

        enemyHealth.animator = animator;
        enemyHealth.player = player;
        enemyHealth.Start();
    }

    [Test]
    public void Health_SetBelowZero_TriggerHitAnimation()
    {
        // Arrange
        float initialHealth = enemyHealth.Health;

        // Act
        enemyHealth.Health = -10;

        // Assert
        Assert.AreNotEqual(initialHealth, enemyHealth.Health);
        Assert.IsTrue(animator.GetBool("alive"));
        Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"));
    }

    [Test]
    public void Health_SetToZeroOrBelow_SetAliveToFalseAndInvokeSlimeDeath()
    {
        // Arrange
        float initialHealth = enemyHealth.Health;

        // Act
        enemyHealth.Health = 0;

        // Assert
        Assert.AreNotEqual(initialHealth, enemyHealth.Health);
        Assert.IsFalse(animator.GetBool("alive"));
        Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"));

        // Wait for the SlimeDeath invocation
        float waitTime = 1.1f;
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
        }

        // Assert
        Assert.IsTrue(enemyHealth == null);
    }

    // Additional tests can be added for OnHit, OnTriggerEnter2D, OnTriggerExit2D, etc.
}

