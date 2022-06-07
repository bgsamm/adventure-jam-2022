using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void LateUpdate() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            transform.position = player.transform.position;
        }
    }
}
