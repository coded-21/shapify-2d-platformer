using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyAwareState
{
    float elapsedTime = 0;

    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.attackCollider.SetActive(true);
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        enemy.attackCollider.SetActive(false);
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (elapsedTime > 3f)
        {
            enemy.SetState(new EnemySearchingState());
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
