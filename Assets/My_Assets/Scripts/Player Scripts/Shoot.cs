using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float gunDamage = 20f;
    public float range = 200f;
    
    public Camera Camera;
    private  AudioEventSender_SFX gunshoot;
    [SerializeField] private GameObject gunShotEFX;

    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        gunshoot = GetComponent<AudioEventSender_SFX>();
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
        //Debug.Log("Player shoots");       
        gunshoot.Play();
        OnEnabledGunshot();
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            
            //Debug.Log(hit.transform.name);
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.Damage();
            }
        }
    }

    private void OnEnabledGunshot()
    {
        //Debug.Log("OnGunShot");
        gunShotEFX.SetActive(true);
        Invoke("OnDisableGunshot", 0.05f);
    }
    private void OnDisableGunshot()
    {
        //Debug.Log("OnDoneGunShot");
        gunShotEFX.SetActive(false);
    }
}
