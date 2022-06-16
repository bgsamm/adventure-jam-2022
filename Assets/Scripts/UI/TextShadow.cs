using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//small class that matches one piece of text with another piece of text
public class TextShadow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI parent;
    private TextMeshProUGUI textComponent;

    private void Awake() {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textComponent.text = parent.text;
    }
}
