using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSystem : MonoBehaviour
{

    PlayerController player;
    [SerializeField] GameObject PlayerAttackCollider;

    // properties
    [SerializeField] float basicAttackCooldown;
    [SerializeField] float basicAttackDamage;
    [SerializeField] float basicAttackKnockback;
    [SerializeField] float basicAttackDuration;

    // getters and setters
    public float BasicAttackDamage { get => basicAttackDamage; }
    public float BasicAttackKnockback { get => basicAttackKnockback; }
    public float BasicAttackDuration { get => basicAttackDuration; }

    // automatic handling
    private float basicAttackLastUsedTime = -5;

    public Ability AbilityOne;
    public Ability AbilityTwo;
    public Ability AbilityThree;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public void UseBasicAttack()
    {
        if (basicAttackLastUsedTime + basicAttackCooldown < Time.time)
        {
            PlayerAttackCollider.SetActive(true);
            basicAttackLastUsedTime = Time.time;
            Debug.Log("Performed a basic attack.");
        } else
        {
            Debug.Log("Basic attack on cooldown.");
        }
    }

    public void UseAbilityOne()
    {
        if (!AbilityOne.IsOnCooldown())
        {
            AbilityOne.UseAbility(player);
        }
        else
        {
            Debug.Log("Ability on cooldown. Time left until usable: " + (AbilityOne.Cooldown - (Time.time - AbilityOne.LastUsedTime)));
        }
    }

    public void UseAbilityTwo()
    {
        if (!AbilityTwo.IsOnCooldown())
        {
            AbilityTwo.UseAbility(player);
        }
        else
        {
            Debug.Log("Ability on cooldown. Time left until usable: " + (AbilityTwo.Cooldown - (Time.time - AbilityTwo.LastUsedTime)));
        }
    }

    public void UseAbilityThree()
    {
        AbilityThree.UseAbility(player);
    }
}