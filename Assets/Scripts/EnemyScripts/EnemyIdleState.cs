using UnityEngine;

public class EnemyIdleState : EnemyUnawareState
{

    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Entered Idle State.");
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        Debug.Log("Exited EnemyIdleState.");
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        // detection is still in progress
        base.UpdateState(enemy);

        // movement will change based on state
    }
}
