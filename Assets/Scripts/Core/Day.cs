using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a class of information about an individual day
//the array of all the days is stored in the Clock script
[System.Serializable]
public class Day
{
    // the bird may be present without a letter
    public bool birdPresent;
    public Letter letter;
    // which NPC shows up at the shop that day
    public NPC npc;
    public string conversationKnot;
    public List<Trade> tradeList;
}
