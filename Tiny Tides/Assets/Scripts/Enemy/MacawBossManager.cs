using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MacawBossManager : MonoBehaviour
{
    public EnemySwordAttack SwordScript;
    public EnemyFlintLock FlintLockScript;
    public EnemySwordPoint SwordRotation;
    public EnemySwordPoint FlintLockRotation;
    private float SwitchingTime = 0f;
    private float TimetoSwitch = 0f;
    public GameObject SwitchTelegraph;
    private bool ParrotSummoned = false;
    public GameObject Enemyparrot;
    private EnemyHealth bosshealth;
    public Slider HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        SwordRotation.UsingSword = true;
        FlintLockRotation.UsingSword = true;
        SwordScript.SwordActive = true;
        FlintLockScript.FlintLockActive = false;
        bosshealth = transform.GetComponent<EnemyHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthBar == null) HealthBar = GameObject.FindGameObjectWithTag("BossBar").GetComponent<Slider>();
        HealthBar.value = bosshealth.currentHealth;
        //transform.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 4f;
        if (ParrotSummoned == false){
            StartCoroutine(SummonParrot());
            ParrotSummoned = true;
        }
        if (SwitchingTime <= 2f)
        {
            TimetoSwitch = Random.Range(5f, 10f);
            
        }
        if (SwitchingTime >= TimetoSwitch){
            StartCoroutine(SwitchWeapons());
            SwitchingTime = -1f;
        }
        SwitchingTime += Time.deltaTime;
        if (PlayerManager.IsOnIsland == false)
        {
            Destroy(gameObject);
        }
    }
IEnumerator SummonParrot()
{
    yield return new WaitForSeconds(5f);
    Instantiate(Enemyparrot, transform.position, transform.rotation);
    yield return new WaitForSeconds(13f);
    ParrotSummoned = false;
}
IEnumerator SwitchWeapons(){
    SwitchTelegraph.SetActive(true);
    yield return new WaitForSeconds(0.5f);
    SwitchTelegraph.SetActive(false);
    if (SwordRotation.UsingSword == false){
            SwordRotation.UsingSword = true;
            SwordRotation.UsingFlintLock = false;
            FlintLockRotation.UsingFlintLock = false;
            FlintLockRotation.UsingSword = true;
            SwordRotation.rotationSpeed = 420f;
            FlintLockRotation.rotationSpeed = 420f;
            SwordScript.SwordActive = true;
            FlintLockScript.FlintLockActive = false;
            transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            transform.GetComponent<EnemyMovement>().enabled = true;
            Rigidbody2D enemyrb = transform.GetComponent<Rigidbody2D>();
            enemyrb.velocity = new Vector2(0f, 0f);
            }
        else if (FlintLockRotation.UsingFlintLock == false){
            SwordRotation.UsingSword = false;
             SwordRotation.UsingFlintLock = true;
            FlintLockRotation.UsingFlintLock = true;
            FlintLockRotation.UsingSword = false;
            SwordRotation.rotationSpeed = 420f;
            FlintLockRotation.rotationSpeed = 420f;
            SwordScript.SwordActive = false;
            FlintLockScript.FlintLockActive = true;
        }
        FlintLockScript.canAttack = false;
        yield return new WaitForSeconds(1f);
        FlintLockScript.canAttack = true;
}
}
