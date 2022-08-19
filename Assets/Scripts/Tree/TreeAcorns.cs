using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAcorns : MonoBehaviour
{
    [SerializeField] GameObject acorns;

    private Clock clock => ResourceLocator.instance.Clock;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting TreeAcorns");

        DontDestroyOnLoad(this.gameObject);

        if ((clock.ActNum == 3 && clock.DayNum == 7 ) || (clock.ActNum == 1 && clock.ActNum == 1))
            acorns.SetActive(true);
        else
            acorns.SetActive(false);
    }
}
