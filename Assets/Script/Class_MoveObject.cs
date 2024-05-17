using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public static MoveObject Instance { get; private set; }
    private Vector3 velocity;
    public bool jumped;

    private void Awake()
    {
        Instance = this;
    }

    //Yatay Düzlemde Hareket Unity Axes-Horizantal
    public void HorizontalControls(float speed)
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += velocity * speed * Time.deltaTime;
    }

    //Dikey Düzlemde Hareket Unity Axes-Vertical
    public void VerticalControls(float speed)
    {
        velocity = new Vector3(0f, Input.GetAxis("Vertical"));
        transform.position += velocity * speed * Time.deltaTime;
    }

    //Yatay + Dikey Düzlemde Hareket
    public void Controls2(float speed)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        var rb = gameObject.GetComponent<Rigidbody2D>();
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");
        rb.MovePosition(
            rb.position + new Vector2(velocity.x, velocity.y) * speed * Time.unscaledDeltaTime
        );
    }

    //Zıplama
    public void Jump(float jump)
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0f))
        {
            rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);
            jumped = true;
        }
        else
        {
            jumped = false;
        }
    }

    //Objeyi Hareket Edilen Yöne Çevirme
    public void GameObjectFlip()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180, 0f);
        }
        else if ((Input.GetAxisRaw("Horizontal") == 1))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
     public void Dash(float dashPower)
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * dashPower, 0f);
    }
}
