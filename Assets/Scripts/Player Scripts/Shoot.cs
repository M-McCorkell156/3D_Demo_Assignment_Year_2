using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float gunDamage = 20f;
    public float range = 200f;
    
    public Camera Camera;

    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerShoots();
        }
    }

    public void PlayerShoots()
    {
        Debug.Log("Player shoots");

        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.Damage(gunDamage);
            }
        }
    }
}
