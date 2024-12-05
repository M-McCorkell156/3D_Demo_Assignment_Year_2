using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingSFX : MonoBehaviour
{
    private AudioEventSender_SFX AudioEventSender_SFX;

    // Start is called before the first frame update
    void Start()
    {
        AudioEventSender_SFX = GetComponent<AudioEventSender_SFX>();
        AudioEventSender_SFX.Play();
    }

}