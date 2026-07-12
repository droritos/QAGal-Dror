using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SortingOrderPlayModeTests
{
    [UnityTest]
    public IEnumerator PlayerAndEnemies_RenderInFrontOfBackground()
    {
        // Give the scene a moment to initialize
        yield return null;

        // Try to find the Background and Player
        GameObject background = GameObject.Find("Nebula Background");
        if (background == null) background = GameObject.Find("Stars Background");
        
        GameObject player = GameObject.FindWithTag("Player");

        // Fail the test if we can't find them!
        Assert.IsNotNull(background, "Could not find a background object in the scene!");
        Assert.IsNotNull(player, "Could not find a player object in the scene!");

        // The background sprite renderer is usually on a child object (Nebula1 or Nebula2)
        SpriteRenderer bgRenderer = background.GetComponentInChildren<SpriteRenderer>();
        Assert.IsNotNull(bgRenderer, "Background is missing a SpriteRenderer in its children!");

        // 1. Test Player Sprite
        SpriteRenderer playerRenderer = player.GetComponentInChildren<SpriteRenderer>();
        Assert.IsNotNull(playerRenderer, "Player is missing a SpriteRenderer!");
        
        AssertIsSortedInFront(playerRenderer, bgRenderer, "The Player's SpriteRenderer must be rendered in front of the Background!");

        // 2. Test Player Particles (like engine exhaust)
        ParticleSystemRenderer[] particleRenderers = player.GetComponentsInChildren<ParticleSystemRenderer>();
        foreach (var psRenderer in particleRenderers)
        {
            AssertIsSortedInFront(psRenderer, bgRenderer, $"The Player's ParticleSystemRenderer '{psRenderer.gameObject.name}' must be rendered in front of the Background!");
        }

        // 3. Test Projectiles (Lasers)
        // FindObjectsOfType with 'true' will find inactive lazers sitting in the Object Pool!
        Projectile[] allProjectiles = Resources.FindObjectsOfTypeAll<Projectile>();
        foreach (var proj in allProjectiles)
        {
            // Make sure it's actually in the scene and not a pure asset
            if (proj.gameObject.scene.name != null)
            {
                SpriteRenderer projRenderer = proj.GetComponentInChildren<SpriteRenderer>();
                if (projRenderer != null)
                {
                    AssertIsSortedInFront(projRenderer, bgRenderer, $"The Projectile '{proj.gameObject.name}' must be rendered in front of the Background!");
                }
            }
        }

        // 4. Test Visual Effects (Explosions, Hits)
        CustomVisualEffect[] allVFX = Resources.FindObjectsOfTypeAll<CustomVisualEffect>();
        foreach (var vfx in allVFX)
        {
            if (vfx.gameObject.scene.name != null)
            {
                // Check Sprite Renderers
                SpriteRenderer[] spriteRenderers = vfx.GetComponentsInChildren<SpriteRenderer>();
                foreach (var sr in spriteRenderers)
                {
                    AssertIsSortedInFront(sr, bgRenderer, $"The VFX Sprite '{vfx.gameObject.name}' must be rendered in front of the Background!");
                }

                // Check Particle Renderers
                ParticleSystemRenderer[] vfxParticles = vfx.GetComponentsInChildren<ParticleSystemRenderer>();
                foreach (var pr in vfxParticles)
                {
                    AssertIsSortedInFront(pr, bgRenderer, $"The VFX Particle '{vfx.gameObject.name}' must be rendered in front of the Background!");
                }
            }
        }

        yield return null;
    }

    private void AssertIsSortedInFront(Renderer frontRenderer, Renderer backRenderer, string errorMessage)
    {
        bool isSortedInFront = false;

        // Unity resolves sorting layers first, then sorting order
        if (SortingLayer.GetLayerValueFromID(frontRenderer.sortingLayerID) > SortingLayer.GetLayerValueFromID(backRenderer.sortingLayerID))
        {
            isSortedInFront = true;
        }
        else if (frontRenderer.sortingLayerID == backRenderer.sortingLayerID)
        {
            if (frontRenderer.sortingOrder > backRenderer.sortingOrder)
            {
                isSortedInFront = true;
            }
            else if (frontRenderer.sortingOrder == backRenderer.sortingOrder)
            {
                // Fallback to Z-position distance from camera
                if (frontRenderer.transform.position.z < backRenderer.transform.position.z)
                {
                    isSortedInFront = true;
                }
            }
        }

        Assert.IsTrue(isSortedInFront, errorMessage);
    }
}
