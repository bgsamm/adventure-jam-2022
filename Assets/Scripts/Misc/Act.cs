using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Act
{
    public Scene scene;
    public Cutscene openingCutscene;
    public Letter openingLetter;
    public List<Day> days;
}
