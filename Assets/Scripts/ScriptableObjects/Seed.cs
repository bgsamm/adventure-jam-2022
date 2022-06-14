using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Scriptable Object/Seed", order = 1)]
public class Seed : Item
{
    public Item yield;
    public Sprite[] gameSprites = new Sprite[3];
    public int daysToGrow = 1;
}

