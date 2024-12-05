using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI waveCounter;
    public Image HeartBar;
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

    public void OnHealthChange(float health)
    {
        //Debug.Log($"ui chnage to {health}");
        HeartBar.fillAmount = health/100f;
    }
}
