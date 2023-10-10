using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationAbility : Ability
{
    // manual settings
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject gatheredObject;
    [SerializeField] private bool hasObject = false;
    [SerializeField] private Vector3 deployOffset;

    // getters and setters
    public override float Cooldown { get => cooldown; protected set => throw new System.NotImplementedException(); }
    public override float LastUsedTime { get => lastUsedTime; protected set => lastUsedTime = value; }

    // automatic handling
    private PlayerController player;
    private float lastUsedTime = -5;
    private BoxCollider2D triggerArea;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        triggerArea = GetComponent<BoxCollider2D>();
    }

    public override bool IsOnCooldown()
    {
        if ((Time.time - LastUsedTime) >= Cooldown) return false;
        else return true;
    }

    public override void UseAbility(PlayerController player)
    {
        if (IsOnCooldown())
        {
            Debug.Log("Ability 3 on Cooldown.");
            return;
        }

        if (!hasObject) TryGatherObject();
        else DeployObject();

        lastUsedTime = Time.time;
    }

    private void TryGatherObject()
    {
        triggerArea.enabled = true;

        Collider2D[] results = new Collider2D[5];
        int numOverlaps = Physics2D.OverlapCollider(triggerArea, new ContactFilter2D(), results);

        if (numOverlaps == 0)
        {
            triggerArea.enabled = false;
            return;
        }

        foreach (Collider2D col in results)
        {
            if (col != null && col.CompareTag("Gatherable"))
            {
                GatherObject(col.gameObject);
                break;
            }
        }

        triggerArea.enabled = false;
    }

    private void GatherObject(GameObject obj)
    {
        gatheredObject = obj;
        obj.SetActive(false);
        hasObject = true;
    }

    private void DeployObject()
    {
        int dir = (player.isFacingRight) ? 1 : -1;
        gatheredObject.transform.position = player.transform.position +
            new Vector3(deployOffset.x * dir, deployOffset.y, deployOffset.z);
        Debug.Log(deployOffset.x * player.transform.right);
        gatheredObject.SetActive(true);
        hasObject = false;
    }
}
