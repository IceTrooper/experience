using System;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public event Action<GameObject> OnDespawn;

    private void OnDisable()
    {
        OnDespawn?.Invoke(gameObject);
    }
}
