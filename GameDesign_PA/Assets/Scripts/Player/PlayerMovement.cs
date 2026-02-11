using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Aktueller Input-Wert für horizontale Bewegung
    private float Move;

    // Rigidbody für physikalische Bewegung
    private Rigidbody2D rb;

    // Bewegungsgeschwindigkeit
    public float speed;

    // SpriteRenderer für Richtungswechsel und Sprite-Wechsel
    private SpriteRenderer sr;

    // Animator für Laufanimation
    public Animator animator;

    // Standard-Sprite (Frontansicht)
    public Sprite normalSprite;

    // Sprite während Interaktionen (Rückenansicht)
    public Sprite backSprite;

    // Wird von Interaktions-Scripts gesetzt
    public bool isInteracting = false;

    void Start()
    {
        // Komponenten beim Start zwischenspeichern
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Wenn der Player sich in einer Interaktion befindet
        if (isInteracting)
        {
            // Animator deaktivieren, damit das BackSprite nicht überschrieben wird
            animator.enabled = false;

            // Rücken-Sprite anzeigen
            sr.sprite = backSprite;

            // Bewegung vollständig stoppen
            rb.linearVelocity = Vector2.zero;

            return;
        }
        else
        {
            // Animator wieder aktivieren, sobald keine Interaktion läuft
            animator.enabled = true;
        }

        // Standard-Sprite anzeigen
        sr.sprite = normalSprite;

        // Horizontale Bewegung einlesen
        Move = Input.GetAxis("Horizontal");

        // Geschwindigkeit auf Rigidbody anwenden
        rb.linearVelocity = new Vector2(Move * speed, rb.linearVelocity.y);

        // Sprite je nach Bewegungsrichtung spiegeln
        if (Move > 0)
            sr.flipX = false;    // Blick nach rechts
        else if (Move < 0)
            sr.flipX = true;     // Blick nach links

        HandleMovement();
    }

    // Setzt den Animator-Parameter für Laufanimation
    private void HandleMovement()
    {
        animator.SetBool("IsWalking", Move != 0);
    }
}
