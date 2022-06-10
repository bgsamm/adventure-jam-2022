using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance { get; private set; }
    private GameObject[] plots; // TODO grab plots from children

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
    }

    public void GrowOvernight()
    {
        foreach(GameObject p in plots)
        {
            Plot currPlot = p.GetComponent<Plot>();
            if (currPlot.occupied)
            {
                currPlot.Grow();
            }
        }
    }
}
