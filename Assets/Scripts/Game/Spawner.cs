using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemie[] Enemies;
    [SerializeField] private float timeBetweenWaves = 5f;

    private float countdown;
    private int waveNummer;
    private bool done = true;

    private Text WAVE;
    private Text waveCountDownText;

    private void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(spawnWave());
            done = false;
            countdown = timeBetweenWaves;
        }
        if(done == true) { countdown -= Time.deltaTime; }

        //waveCountDownText.text = Mathf.Round(countdown).ToString();
        //WAVE.text = "Wave: " + waveNummer;
    }
    IEnumerator spawnWave()
    {
        waveNummer++;

        for (int i = 0; i < waveNummer; i++)
        {
            spawnEnemy(Random.Range(0, Enemies.Length));
            yield return new WaitForSeconds(0.25f);
        }
        done = true;
    }
    void spawnEnemy(int r)
    {
        Instantiate(Enemies[r], transform.position + new Vector3(0, Enemies[r].GetHeigth(), 0), transform.rotation);
    }
}


 