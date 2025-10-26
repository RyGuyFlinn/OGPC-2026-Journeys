using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTwordsMouse : MonoBehaviour
{
    public Vector2 offset;

    private Camera mainCamera;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = player.gameObject.transform.position + new Vector3(offset.x, offset.y, 0);

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
