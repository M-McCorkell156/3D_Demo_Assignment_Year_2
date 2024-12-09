using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public delegate void EventHandler();
    public static event EventHandler OnPlayerAttack;

    private bool isAttacking;
    [SerializeField] private float attackDelayTime;
    //private float enemyDamage = 30f;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
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
        if (!isAttacking)
        {
            StartCoroutine(AttackTimer());
        }
    }

    private IEnumerator AttackTimer()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelayTime);
        //Debug.Log("Attacking again");
        AttackPlayer();
        isAttacking = false;
    }
    public void AttackPlayer()
    {
        if (OnPlayerAttack != null)
        {
            OnPlayerAttack();
        }
    }

}
