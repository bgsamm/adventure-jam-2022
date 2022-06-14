using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class to store individual trades
// (The list of trades available on each day is stored in the Day class)
[System.Serializable]
public class Trade
{
    public ItemStack given;
    public ItemStack received;
    public bool goodTrade;
}
