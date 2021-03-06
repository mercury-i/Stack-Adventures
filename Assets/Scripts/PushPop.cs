using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushPop : MonoBehaviour
{
    private Inventory playerInventory;
    private Inventory stackInventory;
    [SerializeField] private AudioSource pushSound, popSound;
    private GameObject objectivePanel;
    private void Start()
    {
        playerInventory = GetComponent<Inventory>();
        objectivePanel = GameObject.FindGameObjectWithTag("ObjectivePanel");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && stackInventory != null)
        {
            PopItem();
        }
        else if (Input.GetKeyDown(KeyCode.E) && stackInventory != null)
        {
            PushItem();
        }
    }
    private void PushItem()
    {
        if (playerInventory.items.Count > 0)
        {
            if (stackInventory.items.Count < stackInventory.slotsCount)
            {
                // Item to push into the Target stack
                var item = playerInventory.stackUI.transform.GetChild(playerInventory.currentSlot - 1).transform.GetChild(0);
                // Instantiate the item to the Target stack's UI
                Instantiate(item, stackInventory.stackUI.transform.GetChild(stackInventory.currentSlot));
                stackInventory.currentSlot++;
                // Destroying the item from the player's UI
                Destroy(item.gameObject);
                playerInventory.currentSlot--;
                // Adding the item to the Target stack inventory, Removing it from the player's inventory
                stackInventory.items.Push(playerInventory.items.Pop());
                pushSound.Play();
            }
        }
    }
    private void PopItem()
    {   
        if (playerInventory.items.Count == 0)
        {
            if (stackInventory.items.Count > 0)
            {
                // Item to pop from the Target stack
                var item = stackInventory.stackUI.transform.GetChild(stackInventory.currentSlot-1).transform.GetChild(0);
                // Instantiate the item to the player's UI
                Instantiate(item, playerInventory.stackUI.transform.GetChild(playerInventory.currentSlot));
                stackInventory.currentSlot--;
                // Destroying the item from the stack's UI
                Destroy(item.gameObject);
                playerInventory.currentSlot++;
                // Adding the item to the player's inventory, Removing it from the Target stack's inventory
                playerInventory.items.Push(stackInventory.items.Pop());
                popSound.Play();
            }
            else
            {
                // Action to do if pop from empty stack
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stack"))
        {
            // Getting nearest stack object to interact with
            stackInventory = collision.GetComponent<Inventory>();
            collision.transform.Find("UICanvas").GetComponent<Canvas>().enabled = true;
            transform.Find("UICanvas").GetComponent<Canvas>().enabled = true;
            objectivePanel.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stack"))
        {
            collision.transform.Find("UICanvas").GetComponent<Canvas>().enabled = false;
            transform.Find("UICanvas").GetComponent<Canvas>().enabled = false;
            stackInventory = null;
            objectivePanel.SetActive(true);
        }
    }
}
