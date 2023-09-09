using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.awarenessIndicator.SetActive(true);

        // wait for some time and then enter searching state
    }


    public override void ExitState(EnemyStateMachine enemy)
    {
        enemy.awarenessIndicator.SetActive(false);
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        // update code - constantly shoot a ray towards player position and if player is hit, chase player
        Vector3 raycastDir = (enemy.player.transform.position - enemy.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, raycastDir, enemy.awarenessRadius, enemy.nonEnemyLayerMask);

        if (hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                OnPlayerSeen(enemy);
            }
        }

        // Debug
        Debug.DrawLine(enemy.transform.position, enemy.transform.position + (raycastDir * enemy.awarenessRadius), Color.red, Time.deltaTime);
    }

    public virtual void OnPlayerSeen(EnemyStateMachine enemy)
    {
        Debug.Log("Player was seen while enemy is aware.");
        enemy.SetState(new EnemyChaseState());
    }
}
