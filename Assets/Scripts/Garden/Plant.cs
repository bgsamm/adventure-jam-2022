using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant
{
    public bool Watered { get; private set; }
    public bool ReadyToHarvest => growthStage == 2;
    public Sprite Sprite => seed.gameSprites[growthStage];
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
