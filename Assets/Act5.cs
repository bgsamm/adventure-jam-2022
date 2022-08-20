using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Act5 : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [Header("Cutscene")]
    [SerializeField] new private Camera camera;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Vector2[] waypoints;
    [Header("Ending")]
    [SerializeField] private ScreenFader fader;
    [SerializeField] private GameObject endPanel;

    private bool doCutscene;
    private int waypointIndex;

    private Clock clock => ResourceLocator.instance.Clock;

    private void Start() {
        endPanel.SetActive(false);
    }

    private void Update() {
        if (doCutscene) {
            if (waypointIndex < waypoints.Length) {
                var target = waypoints[waypointIndex];
                var offset = new Vector3(target.x, target.y, -10) - camera.transform.position;
                camera.transform.position += cameraSpeed * Time.deltaTime * offset.normalized;
                if (offset.magnitude < 0.01)
                    waypointIndex++;
            }
            else {
                fader.FadeOutEvent = delegate {
                    endPanel.SetActive(true);
                    StartCoroutine(WaitThenEndGame(5));
                };
                StartCoroutine(DelayFade(5));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Trigger entered!");
        PlayerController.playerHasControl = false;
        camera.GetComponent<FollowPlayer>().enabled = false;
        canvas.SetActive(false);
        doCutscene = true;
        waypointIndex = 0;
    }

    private IEnumerator DelayFade(float s) {
        yield return new WaitForSeconds(s);
        fader.FadeOut(true);
    }

    private IEnumerator WaitThenEndGame(float s) {
        yield return new WaitForSeconds(s);
        clock.StartNextAct();
    }
}
