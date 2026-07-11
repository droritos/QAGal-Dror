using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyEditModeTests
{
    [Test]
    public void Enemy_GetDamage_ReducesHealth()
    {
        // Arrange
        GameObject enemyObject = new GameObject();
        Enemy enemy = enemyObject.AddComponent<Enemy>();
        enemy.health = 10;
        enemy.hitEffect = new GameObject(); // Mock hit effect to prevent null reference

        // Act
        enemy.GetDamage(4);

        // Assert
        Assert.AreEqual(6, enemy.health, "Enemy health should be reduced by the damage amount.");
        
        // Cleanup
        GameObject.DestroyImmediate(enemyObject);
    }
}
