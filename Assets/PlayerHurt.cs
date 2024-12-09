using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private Player_Behaviour player;
    private AudioEventSender_SFX damageTaken;

    // Start is called before the first frame update
    void Start()
    {       
        Player_Behaviour.OnPlayerHurt += OnPlayerHurt;
        damageTaken = GetComponent<AudioEventSender_SFX>();

    }

    private void OnPlayerHurt()
    {
        damageTaken.Play(); 
    }
}
