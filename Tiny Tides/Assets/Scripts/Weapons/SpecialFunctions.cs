using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFunctions : MonoBehaviour
{
    
    public static IEnumerator FreezeFrames(AudioClip sound, Transform transform)
    {
        SoundFXManager.Instance.PlaySoundFXClip(sound, transform, 1f, 1.1f);
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + 0.3f;
    
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null; 
        }
      
        Time.timeScale = 1f;
    }
}
