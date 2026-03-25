using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetRotation : MonoBehaviour
{
    public GameObject refrenceObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = refrenceObject.transform.rotation;
    }
}
