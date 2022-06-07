using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void LateUpdate() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            var playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        }
    }
}
