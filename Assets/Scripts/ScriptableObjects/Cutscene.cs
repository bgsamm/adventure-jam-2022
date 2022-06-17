using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cutscene", menuName = "Scriptable Object/Cutscene", order = 0)]
public class Cutscene : ScriptableObject
{
    public List<Sprite> images;
    public float imageDuration;
    public AudioClip audio;
}
