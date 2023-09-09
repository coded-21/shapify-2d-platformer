using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSystem : MonoBehaviour
{

    PlayerController player;

    public Ability AbilityOne;
    public Ability AbilityTwo;
    public Ability AbilityThree;

    private void Start()
    {
        player = GetComponent<PlayerController>();
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