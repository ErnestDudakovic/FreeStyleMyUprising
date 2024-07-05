using UnityEngine;

public class LadderClimbing : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private LayerMask whatIsLadder;
    private bool isClimbing;
    private float originalGravityScale;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player!");
        }

        // Store the original gravity scale
        originalGravityScale = rb.gravityScale;

        // Find the Animator component in the child GameObject
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the player or its children!");
        }
    }

    private void Update()
    {
        if (isClimbing)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
            animator.SetBool("IsClimbing", true);

            // Stop horizontal movement while climbing and set gravity to 0
            rb.gravityScale = 0;
        }
        else
        {
            animator.SetBool("IsClimbing", false);

            // Restore the original gravity scale
            rb.gravityScale = originalGravityScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsLadder) != 0)
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsLadder) != 0)
        {
            isClimbing = false;
        }
    }
}
