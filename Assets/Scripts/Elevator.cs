using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject elevatorCanvas;
    [SerializeField] GameObject elevatorObject;
    [SerializeField] bool isAtTop;
    [SerializeField] float elevation;
    [SerializeField] float elevatorSpeed;

    bool isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            elevatorCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            elevatorCanvas.SetActive(false);
        }
    }

    public void UseElevator() { StartCoroutine(ElevatorCoroutine()); }

    private IEnumerator ElevatorCoroutine()
    {
        if (isActive) { yield break; }
        isActive = true;

        Vector3 startPosition = elevatorObject.transform.localPosition;
        Vector3 endPosition = startPosition;
        endPosition.y = isAtTop ? (endPosition.y - elevation) : (endPosition.y + elevation);

        Debug.Log($"Start position is: {startPosition}");
        Debug.Log($"End position is: {endPosition}");

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elevatorObject.transform.localPosition = currentPosition;

            elapsedTime += elevatorSpeed * Time.deltaTime;
            yield return null;
        }

        elevatorObject.transform.localPosition = endPosition;
        isAtTop = !isAtTop;
        isActive = false;
    }
}
