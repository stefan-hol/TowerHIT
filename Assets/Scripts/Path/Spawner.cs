using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform[] Enemies;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public int waveNummer = 0;
    private bool done = true;

    public Text WAVE;
    public Text waveCountDownText;
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
    }

    void spawnEnemy(int r)
    {
        Instantiate(Enemies[r], spawnPoint.position, spawnPoint.rotation);
    }
}


 