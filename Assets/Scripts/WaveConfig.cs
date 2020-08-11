using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemy = null;
    [SerializeField] private GameObject path = null;
    [SerializeField] private float timeBetweenSpawn = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 2f;

    public GameObject GetEnemy() { return enemy; }

    public List<Transform> GetWaypoints()
    {
        var waveWayPoints = new List<Transform>();

        foreach (Transform child in path.transform)
        {
            waveWayPoints.Add(child);
        }

        return waveWayPoints;
    }

    public float GetTimeBetweenSpawn() { return timeBetweenSpawn; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }

}
