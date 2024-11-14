using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    public GameObject playerObj;
    public NavMeshAgent agent;

    public delegate void EventHandler();
    public static event EventHandler OnEnemyDeath;

    int[] numbers = { 1, 2, 3 };

    private float myHealth = 100f;
    private float playerDamage = 20f;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Testing next wave funcition 
            //Destroy(this.gameObject);

            //Testing taking damage 
            Damage(playerDamage,myHealth);
        }
        */


    }

    private void FixedUpdate()
    {
        Chase();       
        if (Vector3.Distance(playerObj.transform.position,this.transform.position) > 3)
        {
            Chase();
        }
        else
        {
            Invoke("AttackDelay", 2f);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            Debug.Log("Stop");
        }
    }
    private void AttackDelay()
    {
        agent.isStopped = false;
        Debug.Log("Go");

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
        Destroy(this.transform.parent.gameObject);
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
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
