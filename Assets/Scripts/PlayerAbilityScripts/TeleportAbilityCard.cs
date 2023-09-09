using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleportAbilityCard : MonoBehaviour
{
    TeleportAbility ability;
    private int dir;
    [SerializeField] private int phaseOneDist;
    [SerializeField] private int phaseTwoDist;
    [SerializeField] private float phaseOneMoveSpeed;
    [SerializeField] private float phaseTwoMoveSpeed;

    public void Initialize(TeleportAbility teleportAbility)
    {
        ability = teleportAbility;
        dir = ability.dir;
        StartCoroutine(MovementPhaseOne());
    }

    private IEnumerator MovementPhaseOne()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3 (transform.position.x + (dir * phaseOneDist),
            transform.position.y,
            transform.position.z);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            // Calculate the current position based on the lerp value
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);

            // Update the GameObject's position
            transform.position = currentPosition;

            // Increment the elapsed time based on the movement speed
            elapsedTime += Time.deltaTime * phaseOneMoveSpeed;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the GameObject ends up at the exact target position
        transform.position = endPosition;
        StartCoroutine(MovementPhaseTwo());
    }

    private IEnumerator MovementPhaseTwo()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x + (dir * phaseTwoDist),
            transform.position.y,
            transform.position.z);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            // Calculate the current position based on the lerp value
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);

            // Update the GameObject's position
            transform.position = currentPosition;

            // Increment the elapsed time based on the movement speed
            elapsedTime += Time.deltaTime * phaseTwoMoveSpeed;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the GameObject ends up at the exact target position
        transform.position = endPosition;
        ability.Teleport();
    }

    public void Destroy()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
