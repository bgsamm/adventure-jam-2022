using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private GameObject InventoryPanel;

    private InventorySystem Inventory;
    private InventorySlot[] InventorySlots;

    private void Start() {
        Inventory = ResourceLocator.instance.InventorySystem;
        InventorySlots = InventoryPanel.GetComponentsInChildren<InventorySlot>();

        var tradeables = Inventory.stacks.Where(x => x.item.Tradeable).ToArray();
        for (int i = 0; i < InventorySlots.Length; i++) {
            if (i < tradeables.Length)
                InventorySlots[i].SetStack(tradeables[i]);
            else
                InventorySlots[i].SetStack(null);
        }
    }
}
