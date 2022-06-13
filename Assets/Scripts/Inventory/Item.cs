using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item", order = 0)]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite InventorySprite;
    public bool Tradeable;
}
