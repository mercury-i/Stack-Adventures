using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPop : MonoBehaviour
{
    private Inventory playerInventory;
    private Inventory stackInventory;
    private void Start()
    {
        playerInventory = GetComponent<Inventory>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PopItem();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PushItem();
        }
    }
    private void PushItem()
    {
        if (playerInventory.items.Count > 0)
        {
            // Item to push into the Target stack
            var item = playerInventory.stackUI.transform.GetChild(playerInventory.items.Count - 1);
            // Instantiate the item to the Target stack's UI
            Instantiate(item, stackInventory.stackUI.transform);
            // Destroying the item from the player's UI
            Destroy(item.gameObject);
            // Adding the item to the Target stack inventory, Removing it from the player's inventory
            stackInventory.items.Push(playerInventory.items.Pop());
        }
    }
    private void PopItem()
    {
        if (playerInventory.items.Count == 0)
        {
            // Item to pop from the Target stack
            var item = stackInventory.stackUI.transform.GetChild(stackInventory.items.Count - 1);
            // Instantiate the item to the player's UI
            Instantiate(item, playerInventory.stackUI.transform);
            // Destroying the item from the stack's UI
            Destroy(item.gameObject);
            // Adding the item to the player's inventory, Removing it from the Target stack's inventory
            playerInventory.items.Push(stackInventory.items.Pop());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Getting nearest stack object to interact with
        stackInventory = collision.GetComponent<Inventory>();
    }
}
