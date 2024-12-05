using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunting : MonoBehaviour
{

    [SerializeField] public float delayTime;
    private bool isGrunting;

    private AudioEventSender_SFX AudioEventSender_SFX;

    // Start is called before the first frame update
    void Start()
    {
        isGrunting = false;
        AudioEventSender_SFX = GetComponent<AudioEventSender_SFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrunting)
        {
            StartCoroutine(MakeGruntNoise());
        }
    }

    IEnumerator MakeGruntNoise()
    {
        isGrunting = true;
        yield return new WaitForSeconds(delayTime);
        AudioEventSender_SFX.Play();
        //Debug.Log("Grunt");
        isGrunting = false;
    }
}
