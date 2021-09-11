using System.Collections;
using UnityEditor;
using UnityEngine;

public class RoundSpawner : MonoBehaviour
{
    [Tooltip("The smaller value, the faster spawn. RealDelay = Multiplier * EnemyDelay. This value can be used to add more difficulty to spawn ratio.")]
    [SerializeField, Range(0f, 1f)] private float delayMultiplier = 1f;

    [SerializeField] private WeightedList<EnemySpawnData> enemiesSpawnData;

    [SerializeField] private float innerRadius = 1f;
    [SerializeField] private float outerRadius = 3f;

    private bool isStopped;
    private Coroutine spawnCoroutine;

    private void OnEnable()
    {
        spawnCoroutine = StartCoroutine(ContinuousSpawn());
    }

    private void OnDisable()
    {
        StopCoroutine(spawnCoroutine);
    }

    public IEnumerator ContinuousSpawn()
    {
        while(!isStopped)
        {
            var randomEnemyData = enemiesSpawnData.GetRandomItem();
            var randomPosition = GetRandomPositionInsideAnnulus();
            var lookAtCenter = Quaternion.LookRotation(randomPosition - transform.position, transform.up);
            Instantiate(randomEnemyData.enemyPrefab, randomPosition, lookAtCenter, transform);
            yield return new WaitForSeconds(delayMultiplier * randomEnemyData.GetRandomDelay());
        }

        yield return null;
    }

    private Vector3 GetRandomPositionInsideAnnulus()
    {
        var randomPosition = Random.insideUnitCircle;
        randomPosition *= (outerRadius - innerRadius);
        randomPosition += (innerRadius / randomPosition.magnitude) * randomPosition;
        return transform.position + new Vector3(randomPosition.x, 0f, randomPosition.y);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.up, innerRadius);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up, outerRadius);
    }
}
