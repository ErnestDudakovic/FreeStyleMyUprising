using UnityEngine;

public class SimpleElevator : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the elevator moves
    public float targetHeight = 5f; // How high the elevator goes
    public bool isActivated = false; // Flag to check if the elevator is activated (player is on it)
    
    private Vector3 startPosition; // Initial position of the elevator
    private Vector3 endPosition; // Target position when activated

    private void Start()
    {
        startPosition = transform.position; // Remember starting position
        endPosition = startPosition + Vector3.up * targetHeight; // Calculate end position
    }

    private void Update()
    {
        if (isActivated)
        {
            // Move towards the end position
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Move back to the start position
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true; // Activate the elevator when the player steps on it
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = false; // Deactivate the elevator when the player steps off
        }
    }
}
