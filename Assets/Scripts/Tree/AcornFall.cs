using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornFall : MonoBehaviour
{
    [SerializeField] int index;

    GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    Clock clock => ResourceLocator.instance.Clock;

    private void Start() {
        if (clock.ActNum == 3 && clock.DayNum == 7) {
            gameObject.SetActive(!gardenManager.acornFallen[index]);
            if (index == 5) {
                Debug.Log("Acorn 5 fallen");
                gardenManager.acornFallen[index] = true;
            }
        }
        else {
            gameObject.SetActive(false);
        }
    }

    // acorns fall if you click them
    private void OnMouseDown() {
        Debug.Log("Clicked! " + index);

        gardenManager.acornFallen[index] = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

}
