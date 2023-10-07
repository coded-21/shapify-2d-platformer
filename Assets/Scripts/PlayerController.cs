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
    private bool isStunned = false;
    private float lastStunnedTime;
    [SerializeField] private float stunDuration = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCombatSystem = GetComponent<PlayerCombatSystem>();
        Debug.Log("PlayerController Working.");
        isFacingRight = true;
    }

    private void Update()
    {
        if (isStunned && (Time.time - lastStunnedTime >= stunDuration))
        {
            isStunned = false;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        GetInput();


        // this is more of a quick fix, but it works for now. may cause minor bugs later
        //if (!inControl)
        //{
        //    if (isGrounded && (lastLostControlTime + lostControlRecoveryTime) >= Time.time)
        //    {
        //        inControl = true;
        //    }
        //}
    }

    private void GetInput()
    {
        if (isStunned)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        } else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        } else
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
        if (isStunned)
        {
            return;
        }
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

            // exit to main menu
            GameManager.Instance.PlayerDeath();
        }
        else
        {
            health -= damage;
            healthDisplay.text = $"HP: {health}";
        }

        // knockback
        Stun();
        gameObject.GetComponent<Rigidbody2D>()?.AddForce(knockback * dir, ForceMode2D.Impulse);
        Debug.Log("Player got knocked back in " + knockback * dir);
    }

    private void Stun()
    {
        horizontalInput = 0;
        lastStunnedTime = Time.time;
        isStunned = true;
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
