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
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger");
        
        if (other.tag == "Player")
        {
            //Debug.Log("hit player");
            AttackPlayer();
        }
        
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
