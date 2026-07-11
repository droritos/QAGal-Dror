using UnityEngine;

/// <summary>
/// Controls the movement of the Boss ship in various directions.
/// </summary>
public class BossMovement : MonoBehaviour
{
    [Tooltip("Horizontal movement speed")]
    public float moveSpeed = 3f;
    
    [Tooltip("Boundary for horizontal movement")]
    public float horizontalLimit = 6f;
    
    [Tooltip("Downward movement speed multiplier")]
    public float downwardSpeedMultiplier = 0.2f;

    private int direction = 1;

    void Update()
    {
        // Move horizontally
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
        
        // Move slowly downwards
        transform.Translate(Vector3.down * (moveSpeed * downwardSpeedMultiplier) * Time.deltaTime);

        // Change horizontal direction when limits are hit
        if (transform.position.x >= horizontalLimit)
        {
            direction = -1;
        }
        else if (transform.position.x <= -horizontalLimit)
        {
            direction = 1;
        }
    }
}
