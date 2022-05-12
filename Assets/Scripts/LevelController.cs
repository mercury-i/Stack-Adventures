using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject[] levelObjectives;
    [SerializeField] private Transform objectivesUI;
    private int currentObjectiveIndx=0;
    private bool inRange;
    private void Start()
    {
        AddUIObjectives();
    }
    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            PushTargetItem();
        }
    }
    private void PushTargetItem()
    {
        if (playerInventory.items.Count > 0)
        {
            var item = playerInventory.stackUI.transform.GetChild(playerInventory.currentSlot - 1).transform.GetChild(0);
            var currentObjective = levelObjectives[currentObjectiveIndx].GetComponent<DataItem>();
            if (item.GetComponent<DataItem>().item.itemColor == currentObjective.item.itemColor)
            {
                // Success Target Push
                currentObjectiveIndx++;
                Destroy(item.gameObject);
                playerInventory.currentSlot--;
                playerInventory.items.Pop();
                if (isWin())
                {

                }
            }
            else
            {
                // Wrong Target Push

            }
        }
    }
    private void AddUIObjectives()
    {
        for(int i=0;i<levelObjectives.Length;i++)
        {
            Instantiate(levelObjectives[i], objectivesUI.GetChild(i));
            //Debug.Log(objectivesUI.GetChild(i).name);
        }
    }
    private bool isWin()
    {
        return currentObjectiveIndx == levelObjectives.Length;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
