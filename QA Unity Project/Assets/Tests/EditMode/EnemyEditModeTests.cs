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

    [Test]
    public void Enemy_GetDamage_WithShield_ReducesShieldFirst()
    {
        // Arrange
        GameObject enemyObject = new GameObject();
        Enemy enemy = enemyObject.AddComponent<Enemy>();
        enemy.health = 10;
        enemy.shield = 5;
        enemy.hitEffect = new GameObject(); // Mock hit effect

        // Act
        enemy.GetDamage(4);

        // Assert
        Assert.AreEqual(1, enemy.shield, "Enemy shield should be reduced by the damage amount.");
        Assert.AreEqual(10, enemy.health, "Enemy health should remain unchanged while shield is active.");
        
        // Cleanup
        GameObject.DestroyImmediate(enemyObject);
    }

    [Test]
    public void Enemy_GetDamage_WithShield_DamageSpillsOverToHealth()
    {
        // Arrange
        GameObject enemyObject = new GameObject();
        Enemy enemy = enemyObject.AddComponent<Enemy>();
        enemy.health = 10;
        enemy.shield = 5;
        enemy.hitEffect = new GameObject();

        // Act
        enemy.GetDamage(8);

        // Assert
        Assert.AreEqual(0, enemy.shield, "Enemy shield should be fully depleted.");
        Assert.AreEqual(7, enemy.health, "Remaining damage should spill over to health.");
        
        // Cleanup
        GameObject.DestroyImmediate(enemyObject);
    }
}
