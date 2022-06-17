using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicking : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<TreeScene>().ClickedObject(gameObject);
    }
}
