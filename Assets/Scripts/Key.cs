using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] int keyCode;
    [SerializeField] string itemName;

    private void Start()
    {
        itemName = "key" + keyCode;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player.IsInventoryFull())
            {
                Debug.Log("Cant't pick up item. Inventory is full");
            } else
            {
                player.InventoryAddItem(itemName);
                Destroy(gameObject);
            }
        }
    }
}