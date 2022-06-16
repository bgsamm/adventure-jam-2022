using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorns : MonoBehaviour
{
    [SerializeField] private GameObject acorns;
    [SerializeField] private GameObject fallenAcorns;

    private Clock clock => ResourceLocator.instance.Clock;

    //shows the acorns on the tree
    // Start is called before the first frame update
    void Start()
    {
        if (clock.ActNum == 3 && clock.DayNum == 7)
        {
            if (clock.LetterChecked) 
                //since there's no letter that day, it's equivalent to tree visited
            {
                fallenAcorns.SetActive(true);
                acorns.SetActive(false);
            }
            else
            {
                fallenAcorns.SetActive(false);
                acorns.SetActive(true);
            }

        }
    }


}
