using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI waveCounter;
    public Image HeartBar;

    [SerializeField] private Canvas UIcanvas;
    [SerializeField] private Canvas GameOverCanvas;
    [SerializeField] private TMP_Text killCountTMP;
    private int killCount;
    [SerializeField] private TMP_Text waveNoTMP;
    private int waveCount;


    private void Start()
    {

        UIcanvas.gameObject.SetActive(true);
        GameOverCanvas.gameObject.SetActive(false);

        WaveHandler.OnWaveChange += OnWaveChange;
        Enemy.OnEnemyDeath += OnEnemyDeath;
        Player_Behaviour.OnDeath += OnDeath;

        waveCount = 1;
        waveCounter.text = waveCount.ToString();
    }

    private void OnWaveChange()
    {
        waveCount++;
        waveCounter.text = waveCount.ToString();
    }

    private void OnEnemyDeath()
    {
        killCount++;
    }

    private void OnDeath()
    {
        UIcanvas.gameObject.SetActive(false);
        GameOverCanvas.gameObject.SetActive(true);
        killCountTMP.text = $"You killed {killCount} Undead";
        waveNoTMP.text = $"Surived to wave {waveCount}";
    }
    public void OnHealthChange(float health)
    {
        //Debug.Log($"ui chnage to {health}");
        HeartBar.fillAmount = health / 100f;
    }


}
