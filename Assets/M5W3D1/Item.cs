using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SO_Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PL_Inventory inventory))
        {
            inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
