using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    private float Move;
    private Rigidbody2D rb;
    public float speed;

    // Animation
    private SpriteRenderer sr;
    public Animator animator;

    // Sprites
    public Sprite normalSprite;
    public Sprite backSprite;

    // Interaktion
    public bool isInteracting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        // Während Interaktion
        if (isInteracting)
        {
            //Animator stoppen (Damit Backsprite nicht überschrieben wird)
            animator.enabled = false;
            // Back-Sprite anzeigen
            sr.sprite = backSprite;

            rb.linearVelocity = Vector2.zero;

            return;
        }
        else
        {
            // Animator wieder aktivieren
            animator.enabled = true;
        }

        // Normales Sprite anzeigen
            sr.sprite = normalSprite;

        // Movement
        Move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(Move * speed, rb.linearVelocity.y);

        // Player drehen
        if (Move > 0)
            sr.flipX = false;    // nach rechts
        else if (Move < 0)
            sr.flipX = true;     // nach links

        HandleMovement();
    }

    private void HandleMovement()
    {
        animator.SetBool("IsWalking", Move != 0);
    }
}
