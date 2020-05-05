using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public GameObject follow;
    public Vector2 MinCamPos;
    public Vector2 MaxCamPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = follow.transform.position.x;
        float posY = follow.transform.position.y+5.7f;

        transform.position = new Vector3(
            Mathf.Clamp(posX, MinCamPos.x, MaxCamPos.x),
            Mathf.Clamp(posY, MinCamPos.y, MaxCamPos.y),
            transform.position.z);
    }
}
