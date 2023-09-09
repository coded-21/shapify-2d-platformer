using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnawareState : EnemyBaseState

{
    public override void EnterState(EnemyStateMachine enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        // Enemy FOV
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, -enemy.transform.right, enemy.detectionDistance, enemy.playerLayerMask);
        if (hit)
        {
            OnPlayerSeen(enemy);
        }

        // Debug
        Debug.DrawLine(enemy.transform.position, enemy.transform.position + (Vector3.left * enemy.detectionDistance), Color.yellow, Time.deltaTime);
    }

    public virtual void OnPlayerSeen(EnemyStateMachine enemy)
    {
        Debug.Log("Player was hit. Called from unaware state.");
        enemy.SetState(new EnemySuspicionState());
    }
}
