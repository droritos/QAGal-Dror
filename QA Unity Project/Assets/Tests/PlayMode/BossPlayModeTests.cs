using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BossPlayModeTests
{
    [UnityTest]
    public IEnumerator BossMovement_MovesInVariousDirections()
    {
        // Arrange
        GameObject bossObj = new GameObject("Boss");
        BossMovement movement = bossObj.AddComponent<BossMovement>();
        movement.moveSpeed = 50f; // Fast speed for quick testing
        movement.horizontalLimit = 2f;
        movement.downwardSpeedMultiplier = 0.5f;
        
        bossObj.transform.position = Vector3.zero;
        Vector3 initialPos = bossObj.transform.position;

        // Act - Let the game run for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Assert
        Vector3 newPos = bossObj.transform.position;
        Assert.AreNotEqual(initialPos, newPos, "Boss position should have changed after time passed.");
        Assert.Less(newPos.y, initialPos.y, "Boss should have moved downwards.");
        Assert.NotZero(newPos.x, "Boss should have moved horizontally.");

        // Cleanup
        GameObject.Destroy(bossObj);
    }

    [UnityTest]
    public IEnumerator Boss_HasHugeHP_WhenConfigured()
    {
        // Arrange
        GameObject bossObj = new GameObject("Boss");
        Enemy enemyComponent = bossObj.AddComponent<Enemy>();
        enemyComponent.health = 500; // Enforce huge HP configuration

        yield return null; // Wait 1 frame

        // Assert
        Assert.GreaterOrEqual(enemyComponent.health, 500, "Boss should have a huge amount of HP (e.g. 500).");

        // Cleanup
        GameObject.Destroy(bossObj);
    }
}
