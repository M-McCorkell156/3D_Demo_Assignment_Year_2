using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform playPos;
    private float speed = 4f;
    [SerializeField] private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //playPos = GetComponent<>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FollowPlayer()
    {

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
