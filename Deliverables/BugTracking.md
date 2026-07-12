# Bug Tracking & Online Tickets

*Note: You can copy and paste these directly into your GitHub Repository's **Issues** tab to simulate online ticketing.*

---

## [BUG] Shield visual effect does not disappear on WebGL
**Labels:** `bug`, `webgl`, `high-priority`
**Description:** 
When playing the WebGL build, the Enemy's shield sprite remains active even after `shield` reaches 0. This works fine in the Editor, but fails on the browser.
**Steps to Reproduce:**
1. Launch WebGL build.
2. Shoot a shielded enemy until its shield breaks.
3. Observe the blue ring graphic.
**Expected:** The visual should disappear.

---

## [FEATURE] Implement Touch Controls for Mobile
**Labels:** `enhancement`, `mobile`
**Description:** 
Currently, the `PlayerMoving.cs` relies on standard Input Axes (Keyboard/Controller). To pass cross-platform mobile tests, we need to implement `Input.GetTouch()` or a virtual joystick.
**Acceptance Criteria:**
- Player ship moves relative to finger drag on screen.

---

## [BUG] Boss Ship can move out of bounds
**Labels:** `bug`, `logic`
**Description:** 
Depending on screen resolution, the Boss Ship's `horizontalLimit` might push it off-screen, making it impossible for the player to hit it for a few seconds.
**Steps to Reproduce:**
1. Set game view to 4:3 aspect ratio.
2. Wait for Boss to spawn.
3. Observe it moving past the visible edges.
**Expected:** Boss should dynamically calculate camera boundaries or clamp its `horizontalLimit`.

---

## [BUG] All Entities render behind the Nebula Background
**Labels:** `bug`, `graphics`, `critical`
**Description:** 
When the game runs, the Player, Enemies, Lasers, and VFX (explosions) all render behind the background. This happened because every single `SpriteRenderer` and `ParticleSystem` in the project lacked a defined Sorting Layer or Order, causing Unity to render the massive background image over the gameplay.
**Steps to Reproduce:**
1. Start the game.
2. Observe that no ships, lasers, or explosions are visible.
3. Check the Hierarchy; the objects exist and are moving, but are completely hidden behind `Nebula Background`.
**Expected:** The Player, Enemies, Projectiles, and all VFX must explicitly be rendered on a sorting layer in front of the Background layer.
