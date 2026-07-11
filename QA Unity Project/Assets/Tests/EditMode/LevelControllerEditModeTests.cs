using NUnit.Framework;
using UnityEngine;

public class LevelControllerEditModeTests
{
    [Test]
    public void LevelController_HasLevel2ExpansionFields()
    {
        // Arrange
        GameObject obj = new GameObject();
        LevelController controller = obj.AddComponent<LevelController>();

        // Assert
        Assert.IsTrue(controller.enableLevel2, "Level 2 should be enabled by default for the expansion.");
        Assert.Greater(controller.delayBeforeLevel2, 0f, "There should be a delay configured before Level 2 starts.");
        
        // Cleanup
        GameObject.DestroyImmediate(obj);
    }
}
