using UnityEngine;

public class Clock : MonoBehaviour
{
    // Should day count reset to 0 on next act?
    // Could be useful for letter counting / plant growth?
    public static Clock instance { get; private set; }
    public int day { get; private set; }
    public bool isMorning { get; private set; }
    public bool shopVisited;
    
    public const int ACT_LENGTH = 7;

    private void Awake()
    {
        day = 0;
        isMorning = true;   // Game starts in morning

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        shopVisited = false;
    }
    public void NextDay()
    {   // Should be called after/on shop visit, and sleeping
        if (!isMorning)
        {
            ++day;  // Increments day if night already
        }
        isMorning = !isMorning; // flips to morning/dusk

        shopVisited = false;
        GardenManager.instance.GrowOvernight();
        Debug.Log("Next day");
    }
}
