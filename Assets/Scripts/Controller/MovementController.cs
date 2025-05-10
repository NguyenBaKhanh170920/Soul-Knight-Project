using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 mouseWorld;
    public Rigidbody2D Rigidbody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5;

    [Header("Input")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;


    [Header("Sprites")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    //public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;


    void Start()
    {

    }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            Turning();
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }

    }
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

    spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
    spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
    spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
    spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;
    spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
    spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
    spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
    spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 position = Rigidbody.position;
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 translation = movement * speed * Time.unscaledDeltaTime;

        Rigidbody.MovePosition(position + translation);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            activeSpriteRenderer.Flash();
        }
    }
    protected virtual Vector3 GetMousePos()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 vec3 = new Vector3(mouse.x, mouse.y, 0);
        this.mouseWorld = Camera.main.ScreenToWorldPoint(vec3);
        this.mouseWorld.z = 0;
        return this.mouseWorld;
    }
    public void Turning()
    {
        Vector3 mousePosWorld = this.GetMousePos();
        Vector3 direction = mousePosWorld - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                // Facing right
                SetDirection(Vector2.right, spriteRendererRight);
            }
            else
            {
                // Facing left
                SetDirection(Vector2.left, spriteRendererLeft);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                // Facing up
                SetDirection(Vector2.left, spriteRendererUp);
            }
            else
            {
                // Facing down
                SetDirection(Vector2.left, spriteRendererDown);
            }
        }
    }
}
