using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Behaviour : MonoBehaviour, IDamagable
{
    private float myHealth = 100f;
    private float enemyDamage = 30f;

    public Attack Attack;

    // Start is called before the first frame update
    void Start()
    {
        Attack.OnPlayerAttack += TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        //Testing player damage 
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(enemyDamage,myHealth);
        }
        */
    }

    public void Regen()
    {

    }
    public void TakeDamage()
    {

        Damage(enemyDamage);
    }

    public void Damage(float damage)
    {
        //Debug.Log($"player took damage : {damage}");
        myHealth -= damage;
        //Debug.Log($"player health remaining : {myHealth}");

        if (myHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        //Debug.Log("player dead");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
