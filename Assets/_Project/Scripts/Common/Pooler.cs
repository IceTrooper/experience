using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject poolPrefab;
    [SerializeField] private List<GameObject> pool;

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

    public void OnObjectDespawn(GameObject go)
    {
        pool.Add(go);
    }
}
