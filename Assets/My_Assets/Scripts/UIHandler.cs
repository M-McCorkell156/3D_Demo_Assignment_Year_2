using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI waveCounter;

    private int counter;

    //public WaveHandler WaveHandler;

    private void Start()
    {
        WaveHandler.OnWaveChange += OnWaveChange;
        
        counter = 1;
        waveCounter.text = counter.ToString();
    }

    private void OnWaveChange()
    {
        counter++; 
        waveCounter.text = counter.ToString();
    }
}
