using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Seed", menuName = "Scriptable Object/Seed", order = 1)]
public class Seed : Item
{
    public List<ItemStack> yield;
    public Sprite[] gameSprites = new Sprite[3];
    public int daysToGrow = 1;
}

