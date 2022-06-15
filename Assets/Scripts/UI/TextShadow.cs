using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//small class that matches one piece of text with another piece of text
public class TextShadow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI parent;

    // Update is called once per frame
    void Update()
    {
        if (parent.text != this.GetComponent<TextMeshProUGUI>().text)
        {
            this.GetComponent<TextMeshProUGUI>().text = parent.text;
        }
    }
}
