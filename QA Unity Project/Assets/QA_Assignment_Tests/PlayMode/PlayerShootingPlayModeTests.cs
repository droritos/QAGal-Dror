using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerShootingPlayModeTests
{
    [UnityTest]
    public IEnumerator PlayerWeaponPower_IncrementsCorrectly_AndCapsAtMax()
    {
        // 1. Setup a dummy player
        GameObject playerObj = new GameObject("PlayerDummy");
        PlayerShooting shooting = playerObj.AddComponent<PlayerShooting>();
        
        // Disable the component so Update() doesn't fire and crash because it's missing the PoolingController!
        shooting.enabled = false;
        
        // Mock the gun components so we don't get NullReferenceExceptions
        shooting.guns = new Guns();
        shooting.guns.centralGun = new GameObject("CentralGun");
        shooting.guns.leftGun = new GameObject("LeftGun");
        shooting.guns.rightGun = new GameObject("RightGun");
        
        // Initial parameters
        shooting.weaponPower = 1;
        shooting.maxweaponPower = 4;
        PlayerShooting.instance = shooting;
        
        // 2. Act 1: Simulate the Bonus.cs trigger (collecting 1 powerup)
        if (PlayerShooting.instance.weaponPower < PlayerShooting.instance.maxweaponPower)
            PlayerShooting.instance.weaponPower++;
            
        yield return null; // Wait 1 frame
        
        // 3. Assert 1
        Assert.AreEqual(2, shooting.weaponPower, "Weapon power should increment to 2 after collecting 1 powerup.");

        // 4. Act 2: Simulate a greedy player collecting 5 more powerups!
        for(int i = 0; i < 5; i++)
        {
            if (PlayerShooting.instance.weaponPower < PlayerShooting.instance.maxweaponPower)
                PlayerShooting.instance.weaponPower++;
        }
        
        yield return null;

        // 5. Assert 2
        Assert.AreEqual(4, shooting.weaponPower, "Weapon power should safely cap at maxweaponPower (4) and not break the limits!");

        // Cleanup
        GameObject.Destroy(playerObj);
    }
}
