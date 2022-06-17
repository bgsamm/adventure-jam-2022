using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BarterMenu : MonoBehaviour
{
    [Header("Dialog")]
    [SerializeField] private Image characterPortrait;
    [SerializeField] private DialogHandler dialogHandler;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject dontTradeButton;

    [Header("Barter Interface")]
    [SerializeField] private GameObject tradePanel;
    [SerializeField] private GameObject tradeList;
    [SerializeField] private GameObject inventoryPanel;

    [Header(" Trade Summary")]
    [SerializeField] private InventorySlot giveSummarySlot;
    [SerializeField] private InventorySlot getSummarySlot;
    [SerializeField] private Button acceptButton;

    //well, this is inelegant
    [SerializeField] private Item sweetPotato;
    [SerializeField] private Item sugarCane;
    [SerializeField] private Item chile;

    private Clock clock => ResourceLocator.instance.Clock;
    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;
    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;

    private InventorySlot[] inventorySlots;
    private TradeListEntry[] tradeEntries;
    private Trade currentTrade;

    private void Start() {
        var currentDay = clock.CurrentDay;
        characterPortrait.sprite = currentDay.NPC.portraitSprite;

        // clear trade summary
        giveSummarySlot.SetStack(null);
        getSummarySlot.SetStack(null);
        acceptButton.interactable = false;

        // fill in the inventory
        inventorySlots = inventoryPanel.GetComponentsInChildren<InventorySlot>();
        var tradeables = inventory.stacks.Where(x => x.item.Tradeable).ToArray();
        for (int i = 0; i < inventorySlots.Length; i++) {
            var inventorySlot = inventorySlots[i];
            if (i < tradeables.Length)
                inventorySlot.SetStack(tradeables[i]);
            else
                inventorySlot.SetStack(null);
        }

        //special case: Act 2 Day 5 trades are set manually
        if (clock.DayNum == 5 && clock.ActNum == 2)
        {
            ItemStack sweetPotatoStack = inventory.FindStack(sweetPotato);
            ItemStack sugarcaneStack = inventory.FindStack(sugarCane);
            ItemStack chileStack = inventory.FindStack(chile);

            Trade sweetPotatoTrade = new Trade();
            sweetPotatoTrade.given = sweetPotatoStack;
            sweetPotatoTrade.received = null;

            Trade sugarcaneTrade = new Trade();
            sugarcaneTrade.given = sugarcaneStack;
            sugarcaneTrade.received = null;

            Trade chileTrade = new Trade();
            chileTrade.given = chileStack;
            chileTrade.received = null;

            clock.CurrentDay.tradeList.Add(sweetPotatoTrade);
            clock.CurrentDay.tradeList.Add(sugarcaneTrade);
            clock.CurrentDay.tradeList.Add(chileTrade);
        }

        // populate the trade list
        tradeEntries = tradeList.GetComponentsInChildren<TradeListEntry>();
        var trades = currentDay.tradeList;
        for (int i = 0; i < tradeEntries.Length; i++) {
            var tradeEntry = tradeEntries[i];
            if (i < trades.Count) {
                tradeEntry.SetTrade(trades[i]);
                tradeEntry.AddOnClickListener(delegate { SetTradeSummary(tradeEntry.Trade); });
            }
            else {
                //Act 2 Day 5 has no No Trade option
                if (!(clock.DayNum ==5 && clock.ActNum == 2))
                    tradeEntry.SetTrade(null);
            }
        }

        tradePanel.SetActive(false);
        continueButton.SetActive(true);
        dontTradeButton.SetActive(false);
        if (clock.CurrentDay.noTrade)
            dialogHandler.StartDialogue(currentDay.conversationKnot, sceneLoader.LoadGardenScene);
        else
            dialogHandler.StartDialogue(currentDay.conversationKnot, BeginTrading);
    }

    private void SetTradeSummary(Trade trade) {
        giveSummarySlot.SetStack(trade.given);
        getSummarySlot.SetStack(trade.received);
        acceptButton.interactable = inventory.HasItems(trade.given);
        currentTrade = trade;
    }

    private void BeginTrading() {
        tradePanel.SetActive(true);
        continueButton.SetActive(false);
        dontTradeButton.SetActive(true);
    }

    private void EndTrading(bool goodTrade) {
        tradePanel.SetActive(false);
        continueButton.SetActive(true);
        dontTradeButton.SetActive(false);
        // run dialog based on whether the trade was good or not
        string knot = clock.CurrentDay.conversationKnot,
            branch = goodTrade ? "GoodTrade" : "BadTrade";
        dialogHandler.StartDialogue($"{knot}.{branch}", sceneLoader.LoadGardenScene);
    }

    public void AcceptTrade() {
        inventory.RemoveItems(currentTrade.given);
        inventory.AddItems(currentTrade.received);
        EndTrading(currentTrade.goodTrade);
    }

    public void RefuseTrade() {
        EndTrading(false);
    }
}
