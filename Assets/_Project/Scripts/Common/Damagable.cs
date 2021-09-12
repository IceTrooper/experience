using Atoms;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Damagable : MonoBehaviour
{
    public float MaxHealth => maxHealth;
    [SerializeField] private float maxHealth = 10f;

    public float Health => health;
    [SerializeField] private float health = 10f;

    [Space]
    public UnityEvent<float> OnHit;
    public UnityEvent<float> OnDie;

    [Header("AtomEvents (Optional)")]
    [SerializeField] private FloatEvent healthChangedNormalized;

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
