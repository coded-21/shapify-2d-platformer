using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackAbility : Ability
{
    // manual settings
    [SerializeField] GameObject card;
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;
    [SerializeField] private float distanceBuffer;
    [SerializeField] private float cardAttackSpeed;

    // getters and setters
    public override float Cooldown { get => cooldown; protected set => throw new System.NotImplementedException(); }
    public override float LastUsedTime { get => lastUsedTime; protected set => lastUsedTime = value; }
    public float Damage { get => damage; }

    // automatic handling
    private PlayerController player;
    private float lastUsedTime = -5;
    private bool abilityInProgress = false;
    private bool attackInProgress = false;
    public int dir { get; private set; }
    private GameObject _card;
    private Rigidbody2D _cardRB;


    public override bool IsOnCooldown()
    {
        if ((Time.time - LastUsedTime) >= Cooldown)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void UseAbility(PlayerController player)
    {
        this.player = player;
        if (!abilityInProgress)
        {
            if (player.isFacingRight) { dir = 1; }
            else { dir = -1; }

            _card = Instantiate(card, player.transform.position, Quaternion.identity);
            _cardRB = _card.GetComponent<Rigidbody2D>();
            _card.GetComponent<AttackAbilityCard>().Initialize(this);
            abilityInProgress = true;
        }
        else
        {
            StartAttack();
        }
    }

    public async void StartAttack()
    {
        _card.GetComponent<AttackAbilityCard>().EnableAttackCollider();
        await Task.Yield();
        attackInProgress = true;
    }

    private void FixedUpdate()
    {
        if (attackInProgress)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // check if ability end requirements are met; if so, end ability
        if (!((player.transform.position - _card.transform.position).magnitude >= distanceBuffer))
        {
            abilityInProgress = false;
            attackInProgress = false;
            lastUsedTime = Time.time;
            _card.GetComponent<AttackAbilityCard>().Destroy();
        }

        // card attack follow
        Rigidbody2D rbC = _card.GetComponent<Rigidbody2D>();
        Vector2 newPos = Vector2.MoveTowards(_cardRB.position, player.transform.position, cardAttackSpeed * Time.fixedDeltaTime);
        rbC.MovePosition(newPos);
    }
}
