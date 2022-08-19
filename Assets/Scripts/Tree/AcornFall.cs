using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornFall : MonoBehaviour
{
    [SerializeField] int index;

    GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    Clock clock => ResourceLocator.instance.Clock;

    private void Start()
    {
        if (clock.ActNum == 3 && clock.DayNum == 7)
        {
            if (gardenManager.acornFallen[index])
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
            }
            if (index == 5)
            {
                Debug.Log("Acorn 5 fallen");
                gardenManager.acornFallen[index] = true;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown() //acorns fall if you click them
    {
        Debug.Log("Clicked! " + index);

            gardenManager.acornFallen[index] = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

}
