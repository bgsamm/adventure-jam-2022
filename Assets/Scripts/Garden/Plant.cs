using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : Item
{
    [SerializeField] private int daysToGrow = 1;
    private SpriteRenderer plot_sprite;

    private int daysWatered;
    public bool WateredToday { get; private set; }
    public bool Mature { get; private set; }

    public Plant() {
        daysWatered = 0;
        WateredToday = false;
        Mature = false;
        this.Tradeable = true;
    }

    public void Init(string name, Sprite inventorySprite) {
        this.Name = name;
        this.InventorySprite = inventorySprite;
    }

    public void Water() {
        if (!WateredToday) {
            WateredToday = true;
            ++daysWatered;
            Debug.Log("You watered this plant!");
        }
    }

    public void Grow() {
        if (daysWatered == daysToGrow) {
            daysWatered = 0;
            Debug.Log("This plant has grown!");
        }
    }
}

