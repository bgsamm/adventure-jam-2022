using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector2 offset;

    private void LateUpdate() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            transform.position = player.transform.position + new Vector3(offset.x, offset.y, -10);
    }
}
