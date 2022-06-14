using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Image CharacterPortrait;
    [SerializeField] private GameObject TradeList;
    [SerializeField] private GameObject InventoryPanel;
    [Header("Trade Summary")]
    [SerializeField] private InventorySlot GiveSummarySlot;
    [SerializeField] private InventorySlot GetSummarySlot;
    [SerializeField] private Button AcceptButton;

    private Clock clock;
    private InventorySystem inventory;
    private InventorySlot[] inventorySlots;
    private TradeListEntry[] tradeEntries;

    private void Start() {
        clock = ResourceLocator.instance.Clock;
        inventory = ResourceLocator.instance.InventorySystem;

        var currentDay = clock.CurrentDay;
        CharacterPortrait.sprite = currentDay.NPC.portraitSprite;

        // clear trade summary
        GiveSummarySlot.SetStack(null);
        GetSummarySlot.SetStack(null);
        AcceptButton.interactable = false;

        // fill in the inventory
        inventorySlots = InventoryPanel.GetComponentsInChildren<InventorySlot>();
        var tradeables = inventory.stacks.Where(x => x.item.Tradeable).ToArray();
        for (int i = 0; i < inventorySlots.Length; i++) {
            var inventorySlot = inventorySlots[i];
            if (i < tradeables.Length)
                inventorySlot.SetStack(tradeables[i]);
            else
                inventorySlot.SetStack(null);
        }

        // fill the trade list
        tradeEntries = TradeList.GetComponentsInChildren<TradeListEntry>();
        var trades = currentDay.tradeList;
        for (int i = 0; i < tradeEntries.Length; i++) {
            var tradeEntry = tradeEntries[i];
            if (i < trades.Count) {
                tradeEntry.SetTrade(trades[i]);
                tradeEntry.AddOnClickListener(delegate { SetTradeSummary(tradeEntry.Trade); });
            }
            else {
                tradeEntry.SetTrade(null);
            }
        }
    }

    private void SetTradeSummary(Trade trade) {
        GiveSummarySlot.SetStack(trade.given);
        GetSummarySlot.SetStack(trade.received);
        AcceptButton.interactable = true;
    }
}
