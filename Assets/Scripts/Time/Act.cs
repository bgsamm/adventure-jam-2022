using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Act", menuName = "Scriptable Object/Act", order = 0)]
public class Act : ScriptableObject
{
    public string sceneName;
    public Cutscene openingCutscene;
    public Letter openingLetter;
    public List<Day> days;
    public Sprite treePortrait;
    public Sprite treePortraitWatered;
    public Vector2 birdLocation;
    public AudioClip music;
    public AudioClip ambience;
}
