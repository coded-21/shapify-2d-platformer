using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] string requiredKey;
    [SerializeField] GameObject gateObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // check if player has key for this gate by gateCode == requiredKey
            if (collision.GetComponent<PlayerController>().CheckInventoryForItem(requiredKey)) { OpenGate(); }
            else { Debug.Log("Key needed to open this gate."); }
        }
    }

    private void OpenGate()
    {
        gateObject.SetActive(false);
    }
}
