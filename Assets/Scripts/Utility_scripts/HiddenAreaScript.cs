using UnityEngine;

public class HiddenAreaScript : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sr.enabled = false;  // Hide the dark overlay when the player enters
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sr.enabled = true;  // Show the dark overlay when the player exits
        }
    }
}
