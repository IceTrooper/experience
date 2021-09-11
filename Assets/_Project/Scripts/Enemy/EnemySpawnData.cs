using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "IceTrooper/EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    public GameObject enemyPrefab;
    public Vector2 spawnDelay = new Vector2(5, 10);

    public float GetRandomDelay()
    {
        return Random.Range(spawnDelay[0], spawnDelay[1]);
    }
}
