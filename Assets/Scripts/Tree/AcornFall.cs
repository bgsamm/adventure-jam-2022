using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornFall : MonoBehaviour
{
    [SerializeField] int index;

    GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnMouseDown() //acorns fall if you click them
    {
        Debug.Log("Clicked!");
        if (!gardenManager.acornFallen[index])
        {
            gardenManager.acornFallen[index] = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

}
