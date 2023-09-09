using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySuspicionState : EnemyUnawareState
{
    float suspicionThreshold;
    float currentSuspicionLevel;
    float increaseBy = 1f;
    float decreaseBy = 0.4f;
    bool playerInSight;

    public override void EnterState(EnemyStateMachine enemy)
    {
        // enemy.suspicionTrigger.enabled = false;
        enemy.suspicionIndicator.SetActive(true);
        Debug.Log("Enemy is suspicious.");
        suspicionThreshold = enemy.SuspicionThreshold;
        currentSuspicionLevel = 0.2f;
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        // enemy.suspicionTrigger.enabled = true;
        Debug.Log("Exited EnemySuspicionState.");
        enemy.suspicionIndicator.SetActive(false);
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        base.UpdateState(enemy);

        // if susp level > 0 and <= susp threshold:
        // do stuff

        // if suspicion level crosses a threshold, transition to aware state
        if (currentSuspicionLevel >= suspicionThreshold)
        {
            enemy.SetState(new EnemyAwareState());
        }

        // gradually decrease suspicion level if player not seen
        if (playerInSight)
        {
            currentSuspicionLevel += increaseBy * Time.deltaTime;
            playerInSight = false;
            Debug.Log($"Suspicion level: {currentSuspicionLevel}");
        }
        else if(!playerInSight && currentSuspicionLevel > 0)
        {
            currentSuspicionLevel -= decreaseBy * Time.deltaTime;
            currentSuspicionLevel = Mathf.Clamp(currentSuspicionLevel, 0, suspicionThreshold);
            Debug.Log($"Suspicion level: {currentSuspicionLevel}");
        }

        // if suspicion level == 0 for some time, go back to idle or patrol
    }

    public override void OnPlayerSeen(EnemyStateMachine enemy)
    {
        // this is called from the base state

        Debug.Log("Player was seen while enemy is suspicious.");

        // increase suspicion level
        playerInSight = true;
    }
}
