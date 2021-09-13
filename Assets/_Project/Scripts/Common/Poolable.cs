using System;
using UnityEngine;

/// <summary>
/// Class designed for objects supporting pooling.
/// </summary>
public class Poolable : MonoBehaviour
{
    /// <summary>
    /// This event is raised when object is goind to be disabled.
    /// </summary>
    public event Action<GameObject> OnDespawn;

    private void OnDisable()
    {
        OnDespawn?.Invoke(gameObject);
    }
}
