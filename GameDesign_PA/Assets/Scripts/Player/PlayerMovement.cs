using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float Move;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float speed;

    public Animator animator;

    public Sprite normalSprite;
    public Sprite backSprite;

    public bool isInteracting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Sicherheit: Rigidbody resetten (wichtig nach Restart)
        if (rb != null)
            rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (rb == null || sr == null)
            return;

        // 🔴 Wenn Interaktion läuft → komplett stoppen
        if (isInteracting)
        {
            if (animator != null)
                animator.enabled = false;

            sr.sprite = backSprite;

            rb.velocity = Vector2.zero;

            return;
        }
        else
        {
            if (animator != null && !animator.enabled)
                animator.enabled = true;
        }

        sr.sprite = normalSprite;

        Move = Input.GetAxis("Horizontal");

        // ✅ WICHTIG: velocity statt linearVelocity
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (Move > 0)
            sr.flipX = false;
        else if (Move < 0)
            sr.flipX = true;

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (animator != null)
            animator.SetBool("IsWalking", Move != 0);
    }
}