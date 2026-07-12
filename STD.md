# Software Test Description (STD)

## 1. Automated Tests (TDD)

| Test ID | Name | Type | Description | Expected Result |
| :--- | :--- | :--- | :--- | :--- |
| AT-01 | `Enemy_GetDamage_ReducesHealth` | EditMode | Verify damage reduces enemy HP. | Enemy HP drops by exact damage amount. |
| AT-02 | `Enemy_GetDamage_WithShield_ReducesShieldFirst` | EditMode | Verify shield absorbs damage first. | Shield is reduced, health remains unchanged. |
| AT-03 | `Boss_HasHugeHP_WhenConfigured` | PlayMode | Ensure Boss prefab spawns with >500 HP. | Boss initializes with 500+ HP. |
| AT-04 | `BossMovement_MovesInVariousDirections` | PlayMode | Simulate 0.5s time pass for Boss movement. | Boss X/Y coordinates change according to limits. |
| AT-05 | `LevelController_HasLevel2ExpansionFields` | EditMode | Validate Level 2 variables. | `enableLevel2` is true; delay > 0. |
| AT-06 | `PlayerAndEnemies_RenderInFrontOfBackground` | PlayMode | Ensure all sprites and VFX render in front of background. | Sorting layer math asserts player/enemies > background. |
| AT-07 | `PoolingController_ObjectIsReused` | EditMode | Verify Object Pooling system recycles deactivated bullets. | Bullet count remains stable; active object is returned. |
| AT-08 | `PlayerShooting_FireRate_IsRespected` | PlayMode | Verify player respects fire rate limits. | Player cannot shoot faster than fire rate allows. |
| AT-09 | `Boundary_DestroysProjectiles` | PlayMode | Ensure projectiles are destroyed when off-screen. | Object is deactivated when exiting trigger bounds. |

## 2. Manual Test Cases

| Test ID | Module | Description | Steps | Expected Result |
| :--- | :--- | :--- | :--- | :--- |
| MT-01 | Player | Verify player movement bounds. | 1. Move player to left/right screen edges. | Player cannot move beyond screen boundaries. |
| MT-02 | Levels | Verify Level 2 Transition. | 1. Defeat all Level 1 waves.<br>2. Wait 15 seconds. | Level 2 waves (shielded enemies) spawn. |
| MT-03 | Platform | WebGL Build Test. | 1. Download WebGL build from GitHub.<br>2. Run in browser. | Game runs smoothly at 60FPS in browser. |
| MT-04 | Platform | Mobile Build Test. | 1. Open on Touch Device.<br>2. Drag finger on screen. | Universal touch logic moves player ship seamlessly. |
| MT-05 | Visuals | Verify Visual Effects Layering. | 1. Play Level 1.<br>2. Shoot enemies. | Explosions and lasers appear in front of the Nebula Background, not behind it. |
