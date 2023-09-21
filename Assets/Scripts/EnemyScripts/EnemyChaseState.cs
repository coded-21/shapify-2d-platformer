using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyAwareState
{
    PlayerController player;
    private Vector2 targetPosition;
    private float attackDistance;
    bool playerInSight;

    public override void EnterState(EnemyStateMachine enemy)
    {
        player = enemy.player;
        attackDistance = enemy.AttackDistance;
        targetPosition = new Vector2(player.transform.position.x, enemy.transform.position.y);
        enemy.FaceTarget(targetPosition);
        Debug.Log("Entered Chase State");
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        return;
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        base.UpdateState(enemy);

        // Ground Check before moving
        if (!enemy.IsGrounded && !enemy.IsJumping)
        {
            HandleNowhereToGo(enemy);
            return;
        }

        // Chasing Movement Code
        if (Mathf.Abs(targetPosition.x - enemy.transform.position.x) > attackDistance) // still chasing target
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPosition, enemy.moveSpeed * Time.deltaTime);
        }
        else // if target position reached
        {
            if (playerInSight) // and player is visible
            {
                enemy.SetState(new EnemyAttackState());
            }
            else
            {
                enemy.SetState(new EnemySearchingState());
            }
        }

        playerInSight = false;
    }

    public override void OnPlayerSeen(EnemyStateMachine enemy)
    {
        // update target location
        targetPosition = new Vector2(player.transform.position.x, enemy.transform.position.y);
        playerInSight = true;

        // flip enemy direction
        enemy.FaceTarget(targetPosition);
    }

    private void HandleNowhereToGo(EnemyStateMachine enemy)
    {
        // if playerInSight, do one thing

        // if !playerInSight, enter SearchingState
        enemy.SetState(new EnemySearchingState());
    }
}