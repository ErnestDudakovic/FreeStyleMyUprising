using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour, Player.IInteractable
{
    public int coinReward = 10; // Example value, set as needed
    public Text coinMessageText; // Drag and drop your Text element here in the Inspector
    public Image bgImage; // Drag and drop your Image element here in the Inspector
    public float messageDisplayTime = 2.0f; // Duration to display the message
    public AudioClip interactSound; // Drag and drop your AudioClip here in the Inspector

    private Coroutine messageCoroutine;
    private bool isInteracted = false; // Flag to track interaction status
    private AudioSource audioSource; // Reference to the AudioSource component
    private Animator animator; // Reference to the Animator component
    private RectTransform coinMessageRect; // Reference to the RectTransform of coinMessageText
    private RectTransform bgImageRect; // Reference to the RectTransform of bgImage

    private Vector3 originalCoinMessagePos;
    private Vector3 originalBgImagePos;
    private Vector3 targetCoinMessagePos;
    private Vector3 targetBgImagePos;
    private float animationSpeed = 3.0f; // Speed of the animation

    private void Start()
    {
        // Ensure both the text and the background image are hidden initially
        if (coinMessageText != null)
        {
            coinMessageText.gameObject.SetActive(false);
            coinMessageRect = coinMessageText.GetComponent<RectTransform>();
            originalCoinMessagePos = coinMessageRect.localPosition;
        }
        
        if (bgImage != null)
        {
            bgImage.gameObject.SetActive(false);
            bgImageRect = bgImage.GetComponent<RectTransform>();
            originalBgImagePos = bgImageRect.localPosition;
        }
        
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Ensure the chest starts closed
        CloseChest();
    }

    private void CloseChest()
    {
        // Set the animator to play the closed state
        if (animator != null)
        {
            animator.Play("ClosedState");
        }
    }

    public void Interact()
    {
        if (isInteracted)
        {
            Debug.Log("Chest already interacted with. Ignoring interaction.");
            return; // If already interacted, do nothing
        }

        // Increase player's coin count by the value in coinReward
        CoinManager.instance.AddCoins(coinReward);
        Debug.Log("Player interacted with the chest. Coins increased by: " + coinReward);

        // Play the interaction sound
        PlayInteractSound();

        // Show the UI message
        ShowCoinMessage(coinReward);

        // Trigger the chest opening animation
        if (animator != null)
        {
            animator.SetTrigger("OpenChest");
        }
        else
        {
            Debug.LogWarning("Animator component is missing.");
        }

        // Mark as interacted
        isInteracted = true;
    }

    private void ShowCoinMessage(int coinAmount)
    {
        if (coinMessageText != null)
        {
            coinMessageText.text = "+ " + coinAmount + " Coins";
            coinMessageText.gameObject.SetActive(true);
            StartCoroutine(AnimateMessage(true));
        }
        else
        {
            Debug.LogWarning("CoinMessageText is not assigned.");
        }
        
        if (bgImage != null)
        {
            bgImage.gameObject.SetActive(true);
            StartCoroutine(AnimateMessage(false));
        }
        else
        {
            Debug.LogWarning("BgImage is not assigned.");
        }
        
        // If a message is already being displayed, stop the current coroutine
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        // Start a new coroutine to hide the message after a delay
        messageCoroutine = StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator AnimateMessage(bool isText)
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float targetValue = 10f; // Adjust this value to control how much it moves up
        Vector3 originalPos = isText ? originalCoinMessagePos : originalBgImagePos;
        RectTransform rect = isText ? coinMessageRect : bgImageRect;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * animationSpeed;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime);
            rect.localPosition = originalPos + Vector3.up * newValue;
            yield return null;
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        
        if (coinMessageText != null)
        {
            coinMessageText.gameObject.SetActive(false);
            coinMessageRect.localPosition = originalCoinMessagePos; // Reset position
        }
        
        if (bgImage != null)
        {
            bgImage.gameObject.SetActive(false);
            bgImageRect.localPosition = originalBgImagePos; // Reset position
        }
    }

    private void PlayInteractSound()
    {
        if (audioSource != null && interactSound != null)
        {
            audioSource.PlayOneShot(interactSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or InteractSound is not assigned.");
        }
    }
}
