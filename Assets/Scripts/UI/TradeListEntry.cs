using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TradeListEntry : MonoBehaviour
{
    public Trade Trade { get; private set; }

    [SerializeField] private InventorySlot giveSlot;
    [SerializeField] private InventorySlot getSlot;
    [SerializeField] private Image arrows;

    private Button buttonComponent;

    private void Awake() {
        buttonComponent = GetComponent<Button>();
    }

    private void Start() {
        SetTrade(null);
    }

    public void SetTrade(Trade trade) {
        Trade = trade;
        if (trade != null) {
            buttonComponent.interactable = true;
            arrows.enabled = true;
            giveSlot.SetStack(trade.given);
            getSlot.SetStack(trade.received);
        }
        else {
            buttonComponent.interactable = false;
            arrows.enabled = false;
            giveSlot.SetStack(null);
            getSlot.SetStack(null);
        }
    }

    public void AddOnClickListener(UnityAction action) {
        buttonComponent.onClick.AddListener(action);
    }
}
