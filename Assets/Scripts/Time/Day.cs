using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Day
{
    // the bird may be present without a letter
    public bool birdPresent;
    public bool noTrade;
    public Letter letter;
    // which NPC shows up at the shop that day
    public NPC NPC;
    public string conversationKnot;
    public List<Trade> tradeList;
}
