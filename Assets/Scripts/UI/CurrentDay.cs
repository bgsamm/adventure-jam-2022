using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CurrentDay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentDayText;

    private Clock clock => ResourceLocator.instance.Clock;

    private void Update() {
        currentDayText.text = $"Act {clock.ActNum}, Day {clock.DayNum}";
    }
}
