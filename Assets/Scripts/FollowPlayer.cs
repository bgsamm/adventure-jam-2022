using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private int pixelsPerUnit;

    private void LateUpdate() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            transform.position = player.transform.position + new Vector3(offset.x, offset.y, -10);

            //float i_x = Mathf.FloorToInt(transform.localPosition.x * pixelsPerUnit);
            //float i_y = Mathf.FloorToInt(transform.localPosition.y * pixelsPerUnit);
            //float i_z = Mathf.FloorToInt(transform.localPosition.z * pixelsPerUnit);
            //Vector3 p = new Vector3(i_x, i_y, i_z) / pixelsPerUnit;
            //transform.localPosition = p;
        }
    }
}
