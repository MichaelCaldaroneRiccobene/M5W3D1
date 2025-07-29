using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName = "Item/Potions")]
public class SO_HPPotion : SO_Item
{
    [SerializeField] private int AmmountHP = 5;

    public override bool DestroyOnUse(GameObject user)
    {
        return true;
    }

    public override void Use(GameObject user)
    {
        if (user.gameObject.TryGetComponent(out PL_Rb pL_Rb))
        {
            pL_Rb.HP += AmmountHP;
            Debug.Log("Heal");
        }
    }
}
