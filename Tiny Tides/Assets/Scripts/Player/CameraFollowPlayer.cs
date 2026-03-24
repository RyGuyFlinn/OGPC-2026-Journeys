using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform targetPos;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set the cameras position to the target gameobjects position with a z-axis offset
        gameObject.transform.position = new Vector3 (targetPos.position.x, targetPos.position.y, targetPos.position.z - distance);
    }
}
