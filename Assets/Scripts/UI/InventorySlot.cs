using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconComponent;
    [SerializeField] private Image frameComponent;
    [SerializeField] private TextMeshProUGUI countText;

    [HideInInspector] private Eating eating => ResourceLocator.instance.Eating;

    public ItemStack Stack { get; private set; }

    private void Awake() {
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("InventorySlot: click detected");

        if (Stack.item.Edible)
        {
            eating.Eat(Stack);
            Debug.Log(Stack.item.name + " eaten!");
        }
    }
}
