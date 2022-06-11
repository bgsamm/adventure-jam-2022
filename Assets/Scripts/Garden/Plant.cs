using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : Item
{
    [SerializeField] private Sprite[] gameSprites = new Sprite[3];
    [SerializeField] private int daysToGrow = 1;

    private int daysWatered;
    public bool WateredToday { get; private set; }
    public bool Mature { get; private set; }

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

