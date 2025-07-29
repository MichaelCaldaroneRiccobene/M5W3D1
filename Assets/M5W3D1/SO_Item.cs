using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_Item : ScriptableObject
{
    public string ID;
    public string Name;
    public string Description;
    public string Price;

    public abstract void Use(GameObject user);

    public abstract bool DestroyOnUse(GameObject user);
}
