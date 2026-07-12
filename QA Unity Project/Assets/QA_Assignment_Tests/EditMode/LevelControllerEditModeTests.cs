using NUnit.Framework;
using UnityEngine;

public class LevelControllerEditModeTests
{
    [Test]
    public void LevelController_HasLevelDataListInitialized()
    {
        // Arrange
        GameObject obj = new GameObject();
        LevelController controller = obj.AddComponent<LevelController>();

        // Assert
        Assert.IsNotNull(controller.levels, "Levels list should be initialized and not null.");
        
        // Ensure that we can add a level without throwing exceptions
        LevelData testLevel = new LevelData();
        testLevel.delayBeforeLevel = 5f;
        controller.levels.Add(testLevel);
        
        Assert.AreEqual(1, controller.levels.Count, "Should be able to add LevelData to the levels list.");
        Assert.AreEqual(5f, controller.levels[0].delayBeforeLevel, "Level delay should be stored correctly.");

        // Cleanup
        GameObject.DestroyImmediate(obj);
    }
}
