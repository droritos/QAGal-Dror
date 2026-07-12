# Software Test Plan (STP)

## 1. Introduction
This document outlines the test plan for the Unity "Space Shooter" QA mini-project. The objective is to verify that all new features (Shields, Boss, Multi-Level expansion), existing core mechanics, visual rendering, and performance optimizations (Object Pooling) work as intended across multiple platforms.

## 2. Scope
The scope includes both automated (TDD) and manual testing for:
*   Core game mechanics (Player movement, shooting limits, boundary destruction)
*   Enemy Shield Defense
*   Boss Ship functionality (Movement, HP)
*   Multi-Level Orchestration
*   Rendering order & Visual Effects (VFX)
*   Universal Input for Mobile/WebGL Touch controls

## 3. Test Environments
Testing will be performed on the following platforms:
1.  **Unity Editor (PlayMode/EditMode)**: Primary environment for TDD.
2.  **WebGL**: Browser-based testing on Chrome/Edge.
3.  **Mobile (Android)**: Device testing for Universal Touch inputs and performance.

## 4. Test Strategy
*   **Unit/Integration Testing**: Handled via Unity Test Framework (Automated). Tests are stored in `Assets/QA_Assignment_Tests`. Executed continuously via GameCI GitHub Actions (CI/CD).
*   **Functional Testing**: Manual gameplay testing to ensure game loop (Level 1 -> Level 2 -> Level 3 -> Boss) functions without soft-locks.
*   **Non-Functional Testing**: Checking frame rates (FPS) on Mobile and memory leaks over multiple levels via the Object Pooling tests.
*   **Accessibility Testing**: Ensuring the game is playable by users with visual or motor impairments (e.g., auto-fire capabilities).

## 5. Entry and Exit Criteria
*   **Entry**: Code compiles without errors; CI/CD pipeline triggers successfully on push.
*   **Exit**: 100% of automated PlayMode and EditMode tests pass; universal touch controls function on target devices.
