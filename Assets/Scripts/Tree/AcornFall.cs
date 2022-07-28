using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornFall : MonoBehaviour
{
    [SerializeField] int index;


    GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void OnMouseDown() //acorns fall if you click them
    {
        if (!gardenManager.acornFallen[index])
        {
            gardenManager.acornFallen[index] = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

}
