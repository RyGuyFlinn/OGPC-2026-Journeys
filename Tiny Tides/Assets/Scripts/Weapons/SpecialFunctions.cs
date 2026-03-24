using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFunctions : MonoBehaviour
{
    
    public static IEnumerator FreezeFrames(GameObject TestSound = null)
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + 0.3f;
       if (TestSound != null) TestSound.SetActive(true);
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null; 
        }
        if (TestSound != null) TestSound.SetActive(false);
        Time.timeScale = 1f;
    }
}
