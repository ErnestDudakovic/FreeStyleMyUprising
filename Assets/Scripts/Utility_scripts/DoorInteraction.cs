using UnityEngine;
using static Player;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public LevelLoader levelLoader;
    public AudioSource doorAudioSource;

    // Add public fields for the scene indexes
    public int sceneIndexToLoad;

    private void Awake()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        doorAudioSource = GetComponent<AudioSource>();
        if (doorAudioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the door!");
        }
    }

    public void Interact()
    {
        if (sceneIndexToLoad >= 0) // Check if a valid scene index is set
        {
            StartCoroutine(levelLoader.LoadLevel(sceneIndexToLoad));
        }
        else
        {
            Debug.LogWarning("Scene index not set or invalid for: " + gameObject.name);
        }
    }
}
