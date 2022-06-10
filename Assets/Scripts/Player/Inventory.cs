using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }
    public int selectedSeed { get; private set; }
    private int letters;    // Display # letters recieved?
    // Need to add types for seeds, plants, trinkets

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        selectedSeed = 0;
    }

    // General function to add item

    // General function to remove item
}
