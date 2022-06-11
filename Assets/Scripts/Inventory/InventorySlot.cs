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

    public ItemStack Stack { get; private set; }

    /// <summary>
    /// If `stack` is null, clears the slot
    /// </summary>
    public void SetStack(ItemStack stack) {
        Stack = stack;
        iconComponent.enabled = stack != null;
        if (iconComponent.enabled) {
            iconComponent.sprite = stack.item.InventorySprite;
            iconComponent.SetNativeSize();
        }
    }

    public void SetSelected(bool isSelected) {
        frameComponent.enabled = isSelected;
    }
}
