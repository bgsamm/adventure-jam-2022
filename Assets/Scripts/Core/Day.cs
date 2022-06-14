using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a class of information about an individual day
//the array of all the days is stored in the Clock script
[System.Serializable]
public class Day
{
    public bool birdPresent;
    public Letter letterReceived;
    //which NPC shows up at the shop that day
    public NPC npc;
    public string conversationKnot;
    public List<Trade> tradeList;
}
