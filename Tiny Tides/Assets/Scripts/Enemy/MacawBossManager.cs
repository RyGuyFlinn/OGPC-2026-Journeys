using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacawBossManager : MonoBehaviour
{
    public EnemySwordAttack SwordScript;
    public EnemyFlintLock FlintLockScript;
    public EnemySwordPoint SwordRotation;
    public EnemySwordPoint FlintLockRotation;
    // Start is called before the first frame update
    void Start()
    {
        SwordRotation.UsingSword = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && SwordRotation.UsingSword == false){
            SwordRotation.UsingSword = true;
            SwordRotation.UsingFlintLock = false;
            FlintLockRotation.UsingFlintLock = false;
            FlintLockRotation.UsingSword = true;
        }
        else if (Input.GetKeyDown(KeyCode.J) && FlintLockRotation.UsingFlintLock == false){
            SwordRotation.UsingSword = false;
             SwordRotation.UsingFlintLock = true;
            FlintLockRotation.UsingFlintLock = true;
            FlintLockRotation.UsingSword = false;
        }
    }
}
