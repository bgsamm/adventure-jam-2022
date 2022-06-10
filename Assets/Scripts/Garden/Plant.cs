using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : ScriptableObject
{
    [SerializeField] private int daysToGrow;

    private int daysWatered;
    public bool WateredToday { get; private set; }
    public bool Mature { get; private set; }
    
    public Plant() {
        daysWatered = 0;
        WateredToday = false;
        Mature = false;
        Debug.Log("You have planted this plant!");
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

