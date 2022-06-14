using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Letter", menuName = "Scriptable Object/Letter", order = 2)]
public class Letter : ScriptableObject
{
    //[TextArea(maxLines: 40, minLines: 5)]
    [SerializeField]
    public List<string> text;
    public List<ItemStack> gifts;
}
