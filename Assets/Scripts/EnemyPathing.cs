using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // Configuration parameters
    

    // State variables
    private int waypointIndex = 0;
    private List<Transform> waipoints = null;
    private WaveConfig waveConfig = null;

    // Start is called before the first frame update
    void Start()
    {
        waipoints = waveConfig.GetWaypoints();
        transform.position = waipoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig config)
    {
        waveConfig = config;
    }

    private void Move()
    {
        if (waypointIndex < waipoints.Count)
        {
            var targetPoisition = waipoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPoisition, movementThisFrame);

            if (transform.position == targetPoisition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
