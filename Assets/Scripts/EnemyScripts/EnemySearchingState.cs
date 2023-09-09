using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchingState : EnemyAwareState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Looking for the player from searching state.");
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        // exit
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        base.UpdateState(enemy);
    }

    public override void OnPlayerSeen(EnemyStateMachine enemy)
    {
        enemy.SetState(new EnemyChaseState());
    }
}