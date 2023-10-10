using System;
using UnityEngine;

public class TeleportAbility : Ability
{
    [SerializeField] private GameObject card;
    [SerializeField] private float cooldown;
    public override float Cooldown
        {
            get { return cooldown; }
            protected set { throw new NotImplementedException(); }
        }
    private float lastUsedTime = -10;
    public override float LastUsedTime
        {
            get { return lastUsedTime; }
            protected set { lastUsedTime = value; }
        }

    private GameObject _card;
    private PlayerController player;
    public int dir { get; private set; }
    private bool abilityInProgress = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

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
        if (!abilityInProgress)
        {
            if (player.isFacingRight) { dir = 1; }
            else { dir = -1; }

            _card = Instantiate(card, player.transform.position, Quaternion.identity);
            _card.GetComponent<TeleportAbilityCard>().Initialize(this);
            abilityInProgress = true;
        }
        else
        {
            Teleport();
        }
    }

    public void Teleport()
    {
        player.transform.position = _card.transform.position;
        _card.GetComponent<TeleportAbilityCard>().Destroy();
        abilityInProgress = false;
        LastUsedTime = Time.time;
    }
}
