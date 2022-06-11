using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance { get; private set; }
    private GameObject[] plots; // TODO grab plots from children

    [SerializeField] Sprite[] plant_sprites;
    public Plant[] PlantItems { get; private set; }
    
    const string PLOT_TAG = "Plot";

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

        PlantItems = new Plant[plant_sprites.Length];
        for(int p = 0; p < plant_sprites.Length; ++p)
        {
            PlantItems[p] = (Plant)ScriptableObject.CreateInstance("Plant"); 
            PlantItems[p].Init(plant_sprites[p].name, plant_sprites[p]);
        }
        plots = GameObject.FindGameObjectsWithTag(PLOT_TAG);
        foreach(GameObject p in plots)
        {
            p.GetComponent<SpriteRenderer>().enabled = false;
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
