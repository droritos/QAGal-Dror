# Software Test Plan (STP)

## 1. Introduction
This document outlines the test plan for the Unity "Space Shooter" QA mini-project. The objective is to verify that all new features (Shields, Boss, Level 2) and existing core mechanics work as intended across multiple platforms.

## 2. Scope
The scope includes both automated (TDD) and manual testing for:
*   Core game mechanics (Player movement, shooting)
*   Enemy Shield Defense
*   Boss Ship functionality (Movement, HP)
*   Level 2 Orchestration

## 3. Test Environments
Testing will be performed on the following platforms:
1.  **Unity Editor (PlayMode/EditMode)**: Primary environment for TDD.
2.  **WebGL**: Browser-based testing on Chrome/Edge.
3.  **Mobile (Android)**: Device testing for touch inputs and performance.

## 4. Test Strategy
*   **Unit/Integration Testing**: Handled via Unity Test Framework (Automated). Executed continuously via GitHub Actions (CI/CD).
*   **Functional Testing**: Manual gameplay testing to ensure game loop (Level 1 -> Level 2 -> Boss) functions without soft-locks.
*   **Non-Functional Testing**: Checking frame rates (FPS) on Mobile and memory leaks over multiple levels.
*   **Accessibility Testing**: Ensuring the game is playable by users with visual or motor impairments.

## 5. Entry and Exit Criteria
*   **Entry**: Code compiles without errors; CI/CD pipeline triggers successfully.
*   **Exit**: 100% of automated tests pass; 0 critical or high-priority bugs open in the bug tracker.
