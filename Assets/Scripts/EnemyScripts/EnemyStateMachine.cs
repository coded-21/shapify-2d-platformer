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

    [SerializeField] private float attackDistance; public float AttackDistance { get => attackDistance; }
    [SerializeField] private float suspicionThreshold; public float SuspicionThreshold { get => suspicionThreshold; }

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
    }

    public void SetState(EnemyBaseState newState)
    {
        // exit the current state
        currentState?.ExitState(this);

        // set the new state and enter it
        currentState = newState;
        currentState?.EnterState(this);
    }
}
