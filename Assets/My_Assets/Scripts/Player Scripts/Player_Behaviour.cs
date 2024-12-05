using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Behaviour : MonoBehaviour, IDamagable
{
    [SerializeField] private float myHealth;
    private float maxHealth = 100f;
    [SerializeField] private float healthRegenLength;
    [SerializeField] private float healthRegenAmount;
    [SerializeField] private float enemyDamage = 30f;
    private bool isRegen;

    [SerializeField] private GameObject UI;
    private UIHandler UIHandler;


    public Attack Attack;

    // Start is called before the first frame update
    void Start()
    {
        enemyDamage = 30f;
        myHealth = 100f;
        //Debug.Log(myHealth);
        Attack.OnPlayerAttack += TakeDamage;
        UIHandler = UI.GetComponent<UIHandler>();
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
            yield return new WaitForSeconds(healthRegenLength);
            myHealth += healthRegenAmount;
            HealthChange();
        }
        isRegen = false;
    }

    public void HealthChange()
    {
        UIHandler.OnHealthChange(myHealth);
        //Debug.Log($"health change to {myHealth}");
    }
    public void TakeDamage()
    {
        Damage();
    }

    public void Damage()
    {
        //Debug.Log($"player took damage : {damage}");
        myHealth -= enemyDamage;
        HealthChange();
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
