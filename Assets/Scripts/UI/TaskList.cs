using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private GameObject taskPrompt;

    [SerializeField]
    private GameObject[] checks = new GameObject[2];

    private Clock clock => ResourceLocator.instance.Clock;

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.Q)) {
        //    if (taskPanel.activeSelf) {
        //        taskPanel.SetActive(false);
        //        taskPrompt.SetActive(true);
        //    }
        //    else {
        //        taskPanel.SetActive(true);
        //        taskPrompt.SetActive(false);
        //    }
        //}

        //checks[0].SetActive(clock.ShopVisited);
        //checks[1].SetActive(clock.TreeWatered);
        //checks[2].SetActive(clock.LetterChecked);

    }
}
