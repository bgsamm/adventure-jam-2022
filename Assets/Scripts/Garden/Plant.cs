using UnityEngine;

public class Plant
{
    private int daysToGrow;
    private int daysWatered;
    public bool wateredToday { get; private set; }
    public bool mature { get; private set; }
    private int[] daysToGrowMap = { 3, 4, 3, 2, 6, 7 }; // each index for seed

    private Plant() { } // Can't call plant without a seed
    public Plant(int seed)
    {
        daysWatered = 0;
        wateredToday = false;
        mature = false;
        daysToGrow = daysToGrowMap[seed];
        Debug.Log("You have planted this plant!");
    }
    public void Water()
    {
        if (!wateredToday)
        {
            wateredToday = true;
            ++daysWatered;
            Debug.Log("You watered this plant!");
        }
    }
    public void Grow()
    {
        if(daysWatered == daysToGrow)
        {
            daysWatered = 0;
            Debug.Log("This plant has grown!");
        }
    }
}

