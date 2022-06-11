using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image iconComponent;
    [SerializeField] private Image frameComponent;

    /// <summary>
    /// If item is null, clears the slot
    /// </summary>
    public void SetItem(Item item) {
        iconComponent.enabled = item != null;
        if (iconComponent.enabled) {
            iconComponent.sprite = item.InventorySprite;
            iconComponent.SetNativeSize();
        }
    }

    public void SetSelected(bool isSelected) {
        frameComponent.enabled = isSelected;
    }
}
