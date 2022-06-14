using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Letter", menuName = "Scriptable Object/Letter", order = 2)]
public class Letter : ScriptableObject
{
    public List<string> pages;
    public List<ItemStack> gifts;
}
