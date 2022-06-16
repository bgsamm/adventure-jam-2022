using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI controlText;
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private Image[] checks = new Image[3];

    private Clock clock => ResourceLocator.instance.Clock;

    private void Start() {
        taskPanel.SetActive(false);
    }

    private void Update() {
        // Prevent toggling the task list while player control is disabled
        if (!PlayerController.playerHasControl)
            return;

        if (Input.GetButtonDown("Task List")) {
            string text;
            if (!taskPanel.activeSelf) {
                taskPanel.SetActive(true);
                text = controlText.text.Replace("show", "hide");
            }
            else {
                taskPanel.SetActive(false);
                text = controlText.text.Replace("hide", "show");
            }
            controlText.text = text;
        }
        checks[0].enabled = clock.ShopVisited;
        checks[1].enabled = clock.TreeWatered;
        checks[2].enabled = clock.LetterChecked;
    }
}
