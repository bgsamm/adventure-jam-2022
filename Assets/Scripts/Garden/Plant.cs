using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant
{
    public bool Watered { get; private set; }
    public bool ReadyToHarvest => growthStage == seed.daysToGrow;
    public Sprite Sprite => seed.gameSprites[spriteIndex];
    private int spriteIndex => growthStage == seed.daysToGrow ? 2 : (growthStage != 0 ? 1 : 0);
    private int growthStage;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;
    private readonly Seed seed;

    public Plant(Seed seed) {
        this.seed = seed;
        Watered = false;
        growthStage = 0;
    }

    public void Water() {
        Watered = true;
    }

    public void Grow() {
        if (Watered && !ReadyToHarvest) {
            Watered = false;
            growthStage++;
        }
    }

    public void Harvest() {
        foreach (ItemStack stack in seed.yield)
            inventory.AddItems(stack);
    }
}
