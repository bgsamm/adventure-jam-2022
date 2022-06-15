using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [SerializeField]
    private GameObject taskPanel;
    [SerializeField]
    private GameObject taskPrompt;

    [SerializeField]
    private GameObject[] checks = new GameObject[2];

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (taskPanel.activeSelf)
            {
                taskPanel.SetActive(false);
                taskPrompt.SetActive(true);
            }
            else
            {
                taskPanel.SetActive(true);
                taskPrompt.SetActive(false);
            }
        }

        if (ResourceLocator.instance.Clock.ShopVisited)
            checks[0].SetActive(true);
        else
            checks[0].SetActive(false);

        if (ResourceLocator.instance.Clock.TreeWatered)
            checks[1].SetActive(true);
        else
            checks[1].SetActive(false);

        if (ResourceLocator.instance.Clock.LetterChecked)
            checks[2].SetActive(true);
        else
            checks[2].SetActive(false);

    }
}
