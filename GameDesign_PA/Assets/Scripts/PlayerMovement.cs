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

    // Interaktion / Freeze
    private bool isFrozen = false;
    public Sprite lookUpSprite;
    private Sprite defaultSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        defaultSprite = sr.sprite; // Normales Sprite speichern
    }

    void Update()
    {
        // Wenn Spieler eingefroren → keine Bewegung zulassen
        if (isFrozen)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("IsWalking", false);
            return;
        }

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

    // Spieler einfrieren / freigeben
    public void FreezePlayer(bool freeze)
    {
        isFrozen = freeze;
        if (!freeze)
        {
            sr.sprite = defaultSprite; // Zurück zum normalen Sprite
        }
    }

    // Spieler dreht sich nach oben / Richtung Objekt
    public void TurnPlayerAround()
    {
        sr.sprite = lookUpSprite;
    }
}
