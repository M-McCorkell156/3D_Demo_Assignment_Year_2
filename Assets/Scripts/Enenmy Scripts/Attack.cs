using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public delegate void EventHandler();
    public static event EventHandler OnPlayerAttack;

    private float enemyDamage = 30f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Testing giving player damage 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackPlayer();
        }

    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void OnTriggerStay(Collider other)
    {

    }

    public void AttackPlayer()
    {
        if (OnPlayerAttack != null)
        {
            OnPlayerAttack();
        }
    }

}
