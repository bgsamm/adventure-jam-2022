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

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private AudioManager audioManager => ResourceLocator.instance.AudioManager;
    private Clock clock => ResourceLocator.instance.Clock;
    private Inventory inventory => ResourceLocator.instance.InventorySystem;

    private InventorySlot[] inventorySlots;
    private TradeListEntry[] tradeEntries;
    private Trade currentTrade;

    private void Start() {
        var currentDay = clock.CurrentDay;
        characterPortrait.sprite = currentDay.NPC.portraitSprite;

        //TEST--if it doesn't sound good we'll take it out
        audioManager.PlayLoop(clock.CurrentDay.NPC.music);

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

        // TODO: handle this more elegantly
        //special case: Act 2 Day 5 trades are set manually
        if (clock.DayNum == 5 && clock.ActNum == 2) {
            ItemStack sweetPotatoStack = inventory.FindStack(sweetPotato);
            ItemStack sugarcaneStack = inventory.FindStack(sugarCane);
            ItemStack chileStack = inventory.FindStack(chile);

            Trade sweetPotatoTrade = new();
            sweetPotatoTrade.given = sweetPotatoStack;
            sweetPotatoTrade.received = null;

            Trade sugarcaneTrade = new();
            sugarcaneTrade.given = sugarcaneStack;
            sugarcaneTrade.received = null;

            Trade chileTrade = new();
            chileTrade.given = chileStack;
            chileTrade.received = null;

            clock.CurrentDay.tradeList.Add(sweetPotatoTrade);
            clock.CurrentDay.tradeList.Add(sugarcaneTrade);
            clock.CurrentDay.tradeList.Add(chileTrade);
        }

        // populate the trade list
        tradeEntries = tradeList.GetComponentsInChildren<TradeListEntry>();
        var trades = currentDay.tradeList;
        Debug.Log($"BarterMenu: I found {trades.Count} trades");
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

        // Start barter sequence
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
        // TODO: handle this more elegantly
        //Act 2 Day 5 has no No Trade option
        if (!(clock.DayNum == 5 && clock.ActNum == 2))
            dontTradeButton.SetActive(true);
    }

    private void EndTrading(bool goodTrade) {
        tradePanel.SetActive(false);
        continueButton.SetActive(true);
        dontTradeButton.SetActive(false);
        // run dialog based on whether the trade was good or not
        string knot = clock.CurrentDay.conversationKnot,
            branch = goodTrade ? "GoodTrade" : "BadTrade";
        dialogHandler.StartDialogue($"{knot}.{branch}", delegate {
            // reset music
            audioManager.PlayLoop(clock.CurrentAct.music);
            sceneLoader.LoadGardenScene();
        });
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
