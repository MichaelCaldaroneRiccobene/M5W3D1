using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName = "Item/Wepon")]
public class SO_Spada : SO_Item
{
    public override void Use(GameObject user)
    {
        Debug.Log("Attaco");
    }
    public override bool DestroyOnUse(GameObject user)
    {
        return false;
    }
}
