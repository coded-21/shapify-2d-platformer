using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float health;
    [SerializeField] private int inventorySize;
    [SerializeField] TMP_Text healthDisplay;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRadius = 0.1f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] List<string> items = new List<string>();

    PlayerCombatSystem playerCombatSystem;
    public bool isFacingRight { get; private set; }
    Rigidbody2D rb;
    private float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCombatSystem = GetComponent<PlayerCombatSystem>();
        Debug.Log("PlayerController Working.");
        isFacingRight = true;
    }

    private void Update()
    {
        GetInput();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            horizontalInput = 0;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isGrounded)
            {
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            playerCombatSystem.UseAbilityOne();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            playerCombatSystem.UseAbilityTwo();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            playerCombatSystem.UseBasicAttack();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void MoveLeft()
    {
        isFacingRight = false;
        horizontalInput = -1f;
        gameObject.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void MoveRight()
    {
        isFacingRight = true;
        horizontalInput = 1f;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void StopHorizontalMovement()
    {
        horizontalInput = 0;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    public void TakeDamage(float damage, float knockback, Vector2 dir)
    {
        if ((health - damage) <= 0)
        {
            Debug.Log("Player has unalived.");
            healthDisplay.text = $"HP: X_X";
        }
        else
        {
            health -= damage;
            healthDisplay.text = $"HP: {health}";
        }

        // knockback
        gameObject.GetComponent<Rigidbody2D>()?.AddForce(knockback * dir, ForceMode2D.Impulse);
        Debug.Log("Player got knocked back in " + knockback * dir);
    }

    public bool IsInventoryFull()
    {
        return (items.Count < inventorySize) ? false : true;
    }

    public bool CheckInventoryForItem(string item)
    {
        foreach (string inventoryItem in items)
        {
            if (inventoryItem == item)
            {
                return true;
            }
        }
        return false;
    }

    public void InventoryAddItem(string item)
    {
        items.Add(item);
    }

}
