using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movespeed;
    [SerializeField] float jumpspeed;
    bool isGrounded = false;

    PlayerInput player;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = player.actions["Move"].ReadValue<Vector2>();

        rb.linearVelocityX = move.x * movespeed;

        if (player.actions["Jump"].WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocityY = jumpspeed;
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
