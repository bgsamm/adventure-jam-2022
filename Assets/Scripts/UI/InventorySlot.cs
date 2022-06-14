using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image iconComponent;
    [SerializeField] private Image frameComponent;
    [SerializeField] private TextMeshProUGUI countText;

    public ItemStack Stack { get; private set; }

    private void Start() {
        SetStack(null);
    }

    /// <summary>
    /// If `stack` is null, clears the slot
    /// </summary>
    public void SetStack(ItemStack stack) {
        Stack = stack;
        if (stack != null) {
            iconComponent.enabled = true;
            iconComponent.sprite = stack.item.InventorySprite;
            iconComponent.SetNativeSize();
            countText.enabled = true;
            countText.text = stack.count.ToString();
        }
        else {
            iconComponent.enabled = false;
            countText.enabled = false;
        }
    }

    public void SetSelected(bool isSelected) {
        frameComponent.enabled = isSelected;
    }
}
