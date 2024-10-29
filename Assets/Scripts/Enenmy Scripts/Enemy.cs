using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject playerObj;
    public NavMeshAgent agent;

    public delegate void EventHandler();
    public static event EventHandler OnEnemyDeath;

    int[] numbers = { 1, 2, 3 };


    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    private void Update()
    {
        //Testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this.gameObject);
            OnDeath();
        }
    }

    private void FixedUpdate()
    {
        agent.SetDestination(playerObj.transform.position);
        transform.LookAt(playerObj.transform.position);
    }

    //When an enemy dies send event
    private void OnDeath()
    {
        //Debug.Log("on  death");

        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
    }

    #region Observers 
    private List<IObservers> observers = new List<IObservers>();
    private void AddObservers(IObservers observer)
    {
        observers.Add(observer);
    }

    private void RemoveObservers(IObservers observer)
    {
        observers.Remove(observer);
    }
    private void NotifyObservers()
    {
        observers.ForEach((observer) =>
        {
            observer.NotifyObservers();
        });
    }
    #endregion
}
