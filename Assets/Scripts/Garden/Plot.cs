using UnityEngine;

public class Plot : MonoBehaviour
{
    // Need way to store object with different stages of seed/plant
    private int daysWatered;
    public bool readyToHarvest { get; private set; }
    public bool occupied { get; /* just call if seed/plant exists in this */ }
    private int daysWateredToEvolve;

    private void Plant(/* take in seed object here */)
    {
        daysWatered = 0;
    }
    public void Water()
    {
        ++daysWatered;
    }
    public void Grow()
    {
        if(daysWatered == daysWateredToEvolve)
        {
            daysWatered = 0;
            // evolve to next stage of plant
        }
    }
    private void SetDaysToEvolve(int _daysWateredToEvolve)
    {   // Should call when planting, or if days between evolutions vary
        daysWateredToEvolve = _daysWateredToEvolve;
    }
}
