using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputManagerEntry;

public class Enemy : MonoBehaviour, IDamagable
{
    private GameObject playerObj;
    public NavMeshAgent agent;

    public delegate void EventHandler();
    public static event EventHandler OnEnemyDeath;

    private float myHealth;
    private float playerDamage = 100f;

    [SerializeField] private float hitRange;

    [SerializeField] private SkinnedMeshRenderer[] bodyParts;

    [SerializeField] private Material[] skinnedMaterials;
    [SerializeField] private List<Material> tempList;

    private bool isDead;
    private bool isSpawning;
    private bool isAttacking;

    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    [SerializeField] private GameObject DigPartEFX;

    [SerializeField] private GameObject waveHandlerObj;
    [SerializeField] private WaveHandler waveHandler;

    [SerializeField] private CapsuleCollider capsuleCollider;

    [SerializeField] private float attackDelayAmount;

    private float waveNo = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        #region Get Wave Number
        waveHandlerObj = GameObject.FindWithTag("WaveHandler");
        waveHandler = waveHandlerObj.GetComponent<WaveHandler>();

        waveNo = waveHandler.WaveGetter();
        //Debug.Log(waveNo);
        #endregion

        #region Health
        myHealth = 50.0f + (waveNo * 50);
        //Debug.Log(myHealth);
        #endregion

        capsuleCollider = this.GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = true;

        playerObj = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        bodyParts = GetComponentsInChildren<SkinnedMeshRenderer>();

        skinnedMaterials = new Material[bodyParts.Length + 1];

        tempList = new List<Material>(tempList.Count) { };

        isDead = false;
        isSpawning = true;
        isAttacking = false;

        //Debug.Log(bodyParts.Length);
        //Debug.Log(skinnedMaterials.Length);

        #region Skin Mesh Getter
        if (bodyParts != null && skinnedMaterials != null)
        {
            int count = 0;
            //Debug.Log("bodyParts.Length");
            for (int i = 0; i < bodyParts.Length; i++)
            {
                //Debug.Log(bodyParts[i].gameObject.name);
                //Index issue VVV
                //skins = bodyParts[i].materials.ToList();

                //Debug.Log(i);

                //Missing Pelvis material VVV for loop???
                if (bodyParts[i].gameObject.name == "Pelvis")
                {
                    //Debug.Log("This c###");
                    tempList = bodyParts[i].materials.ToList();
                    //Debug.Log(tempList.Count);
                    //i++;
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        //Debug.Log(i);
                        skinnedMaterials[i + j] = tempList[j];
                        //Debug.Log($"Set {i} skin");
                        count += j;
                    }
                }
                else
                {
                    //Debug.Log("no c###");
                    skinnedMaterials[i + count] = bodyParts[i].material;
                }

            }
        }
        #endregion

        Spawning();

    }


    private void FixedUpdate()
    {
        //Chase();

        if (Vector3.Distance(playerObj.transform.position, this.transform.position) > hitRange)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        EnemyChecks();
    }

    private void EnemyChecks()
    {
        //Debug.Log("Check");
        if (isAttacking && !isDead && !isSpawning)
        {
            //Debug.Log("Chase");
            Chase();
        }
        else if (!isDead && !isSpawning)
        {
            //Debug.Log("Delay");
            Invoke("AttackDelay", attackDelayAmount);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            //Debug.Log("Stop");
        }
    }

    #region Spawning
    private void Spawning()
    {
        //Debug.Log("Spawning");
        DigPartEFX.SetActive(true);

        StartCoroutine(SpawnRiseTime());

    }
    private IEnumerator SpawnRiseTime()
    {
        while (this.gameObject.transform.position.y < playerObj.transform.position.y)
        {
            //Debug.Log("Rise Start");
            yield return new WaitForSeconds(0.008f);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 145.3f, transform.position.z), 0.008f);
        }
        //Debug.Log("Done");

        if(!isDead)
        NavSetup();
    }

    #endregion
    private void NavSetup()
    {
        agent.enabled = true;
        switch (waveNo)
        {
            case > 0 and <= 3:
                //Debug.Log("Easy waves");
                agent.speed = 1f;
                agent.acceleration = 1f;
                break;
            case > 3 and <= 6:
                //Debug.Log("Medium waves");
                agent.speed = 2f;
                agent.acceleration = 1.5f;
                break;
            case > 6:
                //Debug.Log("Hard waves");
                agent.speed = 3f;
                agent.acceleration = 2f;
                break;
            default:
                Debug.Log("Error waveNo null");
                break;
        }

        //Debug.Log("NavSetup");
        isSpawning = false;

        Debug.Log($"WaveHanlder Display: \n Enemy Health : {myHealth} \n Enemy speed : {agent.speed}\n Enemy acc : {agent.acceleration}");
    }
    private void AttackDelay()
    {
        agent.isStopped = false;
        //Debug.Log("Go");
    }
    private void Chase()
    {
        //Debug.Log("chasing");
        transform.LookAt(playerObj.transform.position);
        agent.SetDestination(playerObj.transform.position);
    }
    #region Death
    //When an enemy dies delete self and send event
    public void Death()
    {
        capsuleCollider.enabled = false;

        if (!isSpawning)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        isDead = true;
        //Debug.Log("1 enemy death");
        StartCoroutine(DissolveDeath());
    }

    private void Destroy()
    {
        Destroy(this.transform.parent.gameObject);
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
    }

    private IEnumerator DissolveDeath()
    {
        if (skinnedMaterials.Length > 0)
        {
            float counter = 0;
            while (skinnedMaterials[0].GetFloat("_DisolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DisolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
            Destroy();
        }
    }
    #endregion

    #region Damage
    public void Damage()
    {
        //Debug.Log($"enemy took damage : {playerDamage}");

        myHealth -= playerDamage;

        //Debug.Log($"enemy health remaining : {myHealth}");

        if (myHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    #endregion
}
