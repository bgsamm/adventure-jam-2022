using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a class to store individual trades
//the list of trades available on each day is stored in the Day class
[System.Serializable]
public class Trade
{
    Item itemGiven;
    int quantityGiven;
    Item itemReceived;
    int quantityReceived;
    bool goodTrade;
}
