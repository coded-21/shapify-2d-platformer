using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract float Cooldown { get; protected set; }
    public abstract float LastUsedTime { get; protected set; }
    public abstract void UseAbility(PlayerController player);
    public abstract bool IsOnCooldown();
}
