using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    /// <summary>
    /// If item is null, clears the slot
    /// </summary>
    public void SetItem(Item item) {
        icon.enabled = item != null;
        if (icon.enabled) {
            icon.sprite = item.InventorySprite;
            icon.SetNativeSize();
        }
    }
}
