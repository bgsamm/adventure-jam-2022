using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Act", menuName = "Scriptable Object/Act", order = 0)]
public class Act : ScriptableObject
{
    public SceneAsset scene;
    public Cutscene openingCutscene;
    public Letter openingLetter;
    public List<Day> days;
    public Sprite treePortrait;
    public Sprite treePortraitWatered;
    public Vector2 birdLocation;
}
