using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arena : MonoBehaviour
{
    public UnityEvent ArenaStartEvent;
    public UnityEvent ArenaStopEvent;

    public List<WaveData> waves;

    private int currentEnemyCount = 0;
    private int curretnWave = 0;

    private void Start()
    {
        
    }
    

    public void ArenaStart()
    {
        ArenaStartEvent?.Invoke();
        Invoke("SpawnEnemy", 5f);
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < waves[curretnWave].enemies.Count; i++)
        {
            GameObject enemy = Instantiate(waves[curretnWave].enemies[i].EnemyPrefab, waves[curretnWave].enemies[i].EnemySpawnPosition);
            
        }
        Enemy.EnemyDieEventHandler += EnemyDieCallback;
        currentEnemyCount += waves[curretnWave].enemies.Count;
    }

    public void EnemyDieCallback()
    {
        currentEnemyCount--;
        if (currentEnemyCount <= 3&& currentEnemyCount>0)
        {
            if (curretnWave+1 < waves.Count)
            {
                curretnWave++;
                SpawnEnemy();
            }
        }
        else if (currentEnemyCount == 0)
        {
            ArenaStop();
        }
    }

    private void ArenaStop()
    {
        ArenaStopEvent?.Invoke();
        Enemy.EnemyDieEventHandler -= EnemyDieCallback;
    }
}
