using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Scriptable Object/NPC", order = 3)]
public class NPC : ScriptableObject
{
    public new string name;
    public Sprite overworldSprite;
    public Sprite portraitSprite;
    public int musicIndex;
}
