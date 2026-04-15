using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rb;
    Vector2 movement;
    Animator anim;
    SpriteRenderer sr;

    float lastX = 1f;
    bool cercaCaldero = false;
    public bool modoCrafteo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (cercaCaldero && Input.GetKeyDown(KeyCode.Space))
        {
            modoCrafteo = !modoCrafteo;

            if (RecipeManager.instance != null)
                RecipeManager.instance.SetModoCrafteo(modoCrafteo);
        }

        if (modoCrafteo)
        {
            movement = Vector2.zero;
            anim.SetBool("isCharging", true);
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetBool("isCharging", false);
        }

        anim.SetBool("isMoving", movement != Vector2.zero);

        if (movement.x != 0)
        {
            lastX = movement.x;
        }

        sr.flipX = lastX < 0;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Caldero"))
            cercaCaldero = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Caldero"))
        {
            cercaCaldero = false;
            modoCrafteo = false;

            if (RecipeManager.instance != null)
                RecipeManager.instance.SetModoCrafteo(false);
        }
    }
}