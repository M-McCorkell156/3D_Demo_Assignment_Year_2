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
    public event EventHandler OnEnemyDeath;


    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        agent.SetDestination(playerObj.transform.position);
    }

    private void OnDeath()
    {
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(this, EventArgs.Empty);
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
