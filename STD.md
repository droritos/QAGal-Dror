# Software Test Description (STD)

## 1. Automated Tests (TDD)

| Test ID | Name | Type | Description | Expected Result |
| :--- | :--- | :--- | :--- | :--- |
| AT-01 | `Enemy_GetDamage_ReducesHealth` | EditMode | Verify damage reduces enemy HP. | Enemy HP drops by exact damage amount. |
| AT-02 | `Enemy_GetDamage_WithShield_ReducesShieldFirst` | EditMode | Verify shield absorbs damage first. | Shield is reduced, health remains unchanged. |
| AT-03 | `Boss_HasHugeHP_WhenConfigured` | PlayMode | Ensure Boss prefab spawns with >500 HP. | Boss initializes with 500+ HP. |
| AT-04 | `BossMovement_MovesInVariousDirections` | PlayMode | Simulate 0.5s time pass for Boss movement. | Boss X/Y coordinates change according to limits. |
| AT-05 | `LevelController_HasLevel2ExpansionFields` | EditMode | Validate Level 2 variables. | `enableLevel2` is true; delay > 0. |

## 2. Manual Test Cases

| Test ID | Module | Description | Steps | Expected Result |
| :--- | :--- | :--- | :--- | :--- |
| MT-01 | Player | Verify player movement bounds. | 1. Move player to left/right screen edges. | Player cannot move beyond screen boundaries. |
| MT-02 | Levels | Verify Level 2 Transition. | 1. Defeat all Level 1 waves.<br>2. Wait 15 seconds. | Level 2 waves (shielded enemies) spawn. |
| MT-03 | Platform | WebGL Build Test. | 1. Download WebGL build from GitHub.<br>2. Run in browser. | Game runs smoothly at 60FPS in browser. |
| MT-04 | Platform | Mobile Build Test. | 1. Download APK from GitHub.<br>2. Install on Android.<br>3. Test touch inputs. | Player ship follows finger drag; shooting is continuous. |
