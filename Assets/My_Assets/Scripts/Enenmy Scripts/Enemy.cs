using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    private GameObject playerObj;
    public NavMeshAgent agent;

    public delegate void EventHandler();
    public static event EventHandler OnEnemyDeath;

    int[] numbers = { 1, 2, 3 };

    private float myHealth = 100f;
    private float playerDamage = 20f;

    [SerializeField] private float hitRange;

    //[SerializeField] private Material skinnedMesh;
    [SerializeField] private SkinnedMeshRenderer[] bodyParts;

    [SerializeField] private Material[] skinnedMaterials;

    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        bodyParts = GetComponentsInChildren<SkinnedMeshRenderer>();

        if (bodyParts != null)
        {
            //Debug.Log(bodyParts.Length);
            for (int i = 0; i < bodyParts.Length; i++)
            {
                Debug.Log(i);
                skinnedMaterials = bodyParts[i].materials;
            }
        }

    }

    private void FixedUpdate()
    {
        Chase();
        if (Vector3.Distance(playerObj.transform.position, this.transform.position) > hitRange)
        {
            Chase();
        }
        else
        {
            Invoke("AttackDelay", 2f);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            //Debug.Log("Stop");
        }
    }
    private void Spawning()
    {

    }
    private void AttackDelay()
    {
        agent.isStopped = false;
        //Debug.Log("Go");
    }
    private void Chase()
    {
        transform.LookAt(playerObj.transform.position);
        agent.SetDestination(playerObj.transform.position);
    }
    #region Death
    //When an enemy dies delete self and send event
    public void Death()
    {
        //Debug.Log("1 enemy death");
        //Destroy(this.gameObject);
        StartCoroutine(DissolveDeath());
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
    }

    private IEnumerator DissolveDeath()
    {
        if (skinnedMaterials.Length > 0)
        {         
            //Debug.Log("each skin");
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
        }
        //Destroy(this.transform.parent.gameObject);
    }

    private void Destroy()
    {
        //Destroy(this.transform.parent.gameObject);
    }
    #endregion

    #region Damage
    public void Damage(float damage)
    {
        //Debug.Log($"enemy took damage : {damage}");

        myHealth -= damage;

        //Debug.Log($"enemy health remaining : {myHealth}");

        if (myHealth <= 0)
        {
            Death();
        }
    }
    #endregion
}
