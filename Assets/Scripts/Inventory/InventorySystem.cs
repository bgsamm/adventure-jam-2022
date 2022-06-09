using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<ItemStack> stacks;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
}