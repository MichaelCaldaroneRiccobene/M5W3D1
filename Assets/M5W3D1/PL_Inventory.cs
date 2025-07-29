using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Inventory : MonoBehaviour
{
    private List<SO_Item> items = new List<SO_Item>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseItem(1);

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("//// Slot " + i + " ////");
            Debug.Log("Item" + items[i]);
        }
    }

    public void AddItem(SO_Item item)
    {
        items.Add(item);
    }

    private void UseItem(int intex)
    {
        if(intex < items.Count)
        {
            items[intex].Use(gameObject);
            if (items[intex].DestroyOnUse(gameObject)) items.RemoveAt(intex);
        }
    }
}
