using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Behaviour : MonoBehaviour, IDamagable
{
    [SerializeField] private float myHealth = 100f;
    private float maxHealth = 100f; 
    [SerializeField] private float healthRegenLength = 2f;
    [SerializeField] private float enemyDamage = 30f;
    [SerializeField] private bool isRegen;
    [SerializeField] private float healthRegenAmount = 10f;


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
        if (myHealth != maxHealth & !isRegen)
        {
           StartCoroutine(Regen());
        }
    }

    private IEnumerator Regen()
    {
        isRegen = true;
        while (myHealth < maxHealth)
        {
            myHealth += healthRegenAmount;
            yield return new WaitForSeconds(healthRegenLength);
        }
        isRegen = false;
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
