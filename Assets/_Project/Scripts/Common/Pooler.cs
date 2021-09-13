using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling pooling system.
/// </summary>
public class Pooler : MonoBehaviour
{
    /// <summary>
    /// Prefab for creating more objects to pool.
    /// </summary>
    [SerializeField] private GameObject poolPrefab;
    /// <summary>
    /// List of already generated and ready to take objects.
    /// </summary>
    [SerializeField] private List<GameObject> pool;

    /// <summary>
    /// Take the object from pool or just generated.
    /// </summary>
    /// <returns>Object from pool or just generated.</returns>
    public GameObject Spawn()
    {
        GameObject go = null;
        if(pool.Count > 0)
        {
            go = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            go.SetActive(true);
        }
        else
        {
            go = Instantiate(poolPrefab, transform);
            var goPoolable = go.GetComponent<Poolable>();
            goPoolable.OnDespawn += OnObjectDespawn;
        }

        return go;
    }

    /// <summary>
    /// Method used to collect used objects.
    /// </summary>
    /// <param name="go">No longer needed object.</param>
    public void OnObjectDespawn(GameObject go)
    {
        pool.Add(go);
    }
}
