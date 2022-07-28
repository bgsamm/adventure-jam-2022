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
            //since there's no letter that day, it's equivalent to tree visited
            bool treeVisited = gardenManager.LetterChecked;

            for(int x = 0; x < 8; x++)
            {
                acorns[x].SetActive(!gardenManager.acornFallen[x]);
                fallenAcorns[x].SetActive(gardenManager.acornFallen[x]);

                //inelegant, but a way of handling the acorns not visible in the Tree scene
                if (gardenManager.acornFallen[1])
                    gardenManager.acornFallen[2] = true;
                if (gardenManager.acornFallen[3])
                    gardenManager.acornFallen[4] = true;
                if (gardenManager.acornFallen[6])
                    gardenManager.acornFallen[7] = true;
                if (gardenManager.acornFallen[8])
                    gardenManager.acornFallen[9] = true;
            }
            
        }
    }
}
