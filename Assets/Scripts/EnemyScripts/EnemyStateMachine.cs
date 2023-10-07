using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // Dependencies
    public PlayerController player { get; private set; }
    public CircleCollider2D suspicionTrigger;
    public CircleCollider2D awarenessTrigger;
    public GameObject suspicionIndicator;
    public GameObject awarenessIndicator;
    public GameObject attackCollider;
    public LayerMask playerLayerMask;
    public LayerMask nonEnemyLayerMask;

    // Enemy Properties
    public float moveSpeed;
    public float detectionDistance;
    public float awarenessRadius;
    public float enemyAttackCooldown;
    public bool isFacingRight;
    public bool isObjectDestroyer;

    [SerializeField] private float attackDistance;
    [SerializeField] private float suspicionThreshold;
    [SerializeField] private float groundCheckRadius = 5f;
    [SerializeField] private float wallCheckRadius = 3f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform furtherGroundCheck;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [Range(1, 10)]
    [SerializeField] private float jumpDirectionConfig;
    private float lastJumpTime;


    // Getters
    public float AttackDistance { get => attackDistance; }
    public float SuspicionThreshold { get => suspicionThreshold; }
    public bool IsGrounded { get => isGrounded; }
    public bool IsJumping { get => isJumping; }

    // Enemy States
    private EnemyBaseState currentState;
    EnemyIdleState IdleState = new EnemyIdleState();
    EnemyChaseState ChaseState = new EnemyChaseState();
    EnemySuspicionState SuspicionState = new EnemySuspicionState();
    EnemyAwareState AwareState = new EnemyAwareState();

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        SetState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        GroundCheck();
        WallCheck();
    }

    public void SetState(EnemyBaseState newState)
    {
        // exit the current state
        currentState?.ExitState(this);

        // set the new state and enter it
        currentState = newState;
        currentState?.EnterState(this);
    }

    private void GroundCheck()
    {
        // check for immediate ground
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, groundLayerMask);
        Debug.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckRadius, groundCheck.position.z),
            color: Color.green);

        if (hit)
        {
            isGrounded = true;
            isJumping = false;
        } else
        {
            FurtherGroundCheck();
        }
    }

    private void FurtherGroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(furtherGroundCheck.position, Vector2.down, groundCheckRadius, groundLayerMask);
        Debug.DrawLine(furtherGroundCheck.position,
            new Vector3(furtherGroundCheck.position.x, furtherGroundCheck.position.y - groundCheckRadius, furtherGroundCheck.position.z),
            color: Color.green);

        if (hit) // there is further ground within range and enemy can jump to it
        {
            HandleJump();
        } else // there is no further ground within range
        {
            isGrounded = false;
            isJumping = false;
        }
    }

    private void ObstacleCheck()
    {
        // check for obstacle in path
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, transform.right, wallCheckRadius, groundLayerMask);

        // debug line
        Debug.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x - wallCheckRadius, groundCheck.position.y, groundCheck.position.z),
            color: Color.cyan);

        // if obstacle found, check if it is a wall
        if (hit)
        {
            HandleObstacleInteraction();
        }
    }

    private void WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckRadius, groundLayerMask);
        Debug.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x - wallCheckRadius, wallCheck.position.y, wallCheck.position.z),
            color: Color.cyan);

        if (hit) // obstacle is a wall
        {
            Debug.Log("Hit a wall.");
        } else // no wall jump over obstacle or destroy it
        {
            ObstacleCheck();
        }
    }

    private void HandleObstacleInteraction()
    {
        if (isObjectDestroyer)
        {
            // destroy the object; attack the object
        } else
        {
            HandleJump();
        }
    }

    private void HandleJump()
    {
        if (lastJumpTime + jumpCooldown <= Time.time)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(
                jumpForce * (Vector3.up + (transform.right/jumpDirectionConfig)).normalized, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
            isJumping = true;
        }
    }

    public void FaceTarget(Vector2 targetPosition)
    {
        if ((targetPosition.x - transform.position.x) > 0)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }

        int dir = isFacingRight ? 0 : 180;
        transform.rotation = Quaternion.Euler(0, dir, 0);
    }
}
