using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WaveData 
{
    [SerializeField] public List<EnemyData> enemies;
}
[System.Serializable]
public class EnemyData
{
    [SerializeField] public GameObject EnemyPrefab;
    [SerializeField] public Transform EnemySpawnPosition;
}

