using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Cutscene
{
    public string name;
    public List<Sprite> images;
    public AudioSource audio;
}
