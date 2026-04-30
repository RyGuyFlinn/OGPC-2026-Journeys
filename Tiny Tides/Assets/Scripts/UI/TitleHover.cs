using System.Collections;
using System.Collections.Generic;
//using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class TitleHover : MonoBehaviour
{
    public float centerY;
    public float amplitude;
    public float hoverTime;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //advance timer
        time += 0.02f;
        if (time >= hoverTime)
        {
            time = 0;
        }

        transform.position = new Vector3(transform.position.x, 
            centerY + amplitude * Mathf.Sin(360 * (time / hoverTime)), 
            0);
    }
}
