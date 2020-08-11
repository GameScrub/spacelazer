using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private List<WaveConfig> waveConfigs = null;
    [SerializeField] private int startingWave = 0;
    [SerializeField] private bool looping = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {

        for (int i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            var enemy = Instantiate(
                currentWave.GetEnemy(),
                currentWave.GetWaypoints()[0].transform.position,
                Quaternion.identity);

            enemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);

            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawn());
        }
    }
    
}
