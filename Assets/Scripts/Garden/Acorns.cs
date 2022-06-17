using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorns : MonoBehaviour
{
    [SerializeField] private GameObject acorns;
    [SerializeField] private GameObject fallenAcorns;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    // Determines whether to show the acorns on the tree
    void Start() {
        if (clock.ActNum == 3 && clock.DayNum == 7) {
            //since there's no letter that day, it's equivalent to tree visited
            bool treeVisited = gardenManager.LetterChecked;
            acorns.SetActive(!treeVisited);
            fallenAcorns.SetActive(treeVisited);
        }
    }
}
