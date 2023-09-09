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

    [SerializeField] TMP_Text healthDisplay;

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
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            horizontalInput = 0;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
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
}
