using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Test
    private float Move;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    //private float input;
    
    public float speed;
    public Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Bewegung
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
        if (Move != 0)
            {
            animator.SetBool("IsWalking", true);
            }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
