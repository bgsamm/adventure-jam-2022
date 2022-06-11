using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string Name;
    public Sprite InventorySprite;
    public bool Tradeable { get; protected set; }
}
