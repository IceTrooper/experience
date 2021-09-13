using Atoms;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Adds the ability to receive damage.
/// </summary>
[DisallowMultipleComponent]
public class Damagable : MonoBehaviour
{
    /// <summary>
    /// Get the max health.
    /// </summary>
    public float MaxHealth => maxHealth;
    [SerializeField] private float maxHealth = 10f;

    /// <summary>
    /// Get the current health.
    /// </summary>
    public float Health => health;
    [SerializeField] private float health = 10f;

    /// <summary>
    /// Event when object is hit.
    /// </summary>
    [Space]
    public UnityEvent<float> OnHit;
    /// <summary>
    /// Event when object die.
    /// </summary>
    public UnityEvent<float> OnDie;

    /// <summary>
    /// Event for normalized health value (health/maxHealth). Useful for health bars.
    /// </summary>
    [Header("AtomEvents (Optional)")]
    [SerializeField] private FloatEvent healthChangedNormalized;

    /// <summary>
    /// Take damage from caller.
    /// </summary>
    /// <param name="damageAmount">Amount of damage</param>
    /// <returns></returns>
    public float TakeDamage(float damageAmount)
    {
        if(health <= 0f) return 0f;

        health -= damageAmount;

        if(health <= 0f)
        {
            damageAmount += health;
            health = 0f;
            OnDie.Invoke(damageAmount);
        }
        else
        {
            OnHit.Invoke(damageAmount);
        }

        if(healthChangedNormalized != null) healthChangedNormalized.Raise(health / maxHealth);
        return damageAmount;
    }

    /// <summary>
    /// Heal the object.
    /// </summary>
    /// <param name="healAmount">Healing amount</param>
    /// <returns></returns>
    public float Heal(float healAmount)
    {
        if(health >= maxHealth) return 0;

        health += healAmount;

        if(health >= maxHealth)
        {
            healAmount -= health - maxHealth;
            health = maxHealth;
        }

        if(healthChangedNormalized != null) healthChangedNormalized.Raise(health / maxHealth);
        return healAmount;
    }
}
