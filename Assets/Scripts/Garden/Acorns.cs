using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorns : MonoBehaviour
{
    [SerializeField] private GameObject[] acorns = new GameObject[9];
    [SerializeField] private GameObject[] fallenAcorns = new GameObject[9];

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    // Determines whether to show the acorns on the tree
    void Start() {
        if (clock.ActNum == 3 && clock.DayNum == 7) {
            Debug.Log("Displaying acorns");
            //inelegant, but a way of handling the acorns not visible in the Tree scene
            if (gardenManager.acornFallen[0])
                gardenManager.acornFallen[1] = true;
            if (gardenManager.acornFallen[5])
                gardenManager.acornFallen[6] = true;
            if (gardenManager.acornFallen[7])
                gardenManager.acornFallen[8] = true;

            for (int x = 0; x < 9; x++) {
                Debug.Log("Acorn " + x + " = " + gardenManager.acornFallen[x]);

                acorns[x].SetActive(!gardenManager.acornFallen[x]);
                fallenAcorns[x].SetActive(gardenManager.acornFallen[x]);
            }
        }
        else {
            for (int x = 0; x < 9; x++) {
                acorns[x].SetActive(false);
                fallenAcorns[x].SetActive(false);
            }
        }
    }
}
