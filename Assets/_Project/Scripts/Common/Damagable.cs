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

    public float TakeDamage(float damageAmount)
    {
        if(health <= 0f) return 0f;

        health -= damageAmount;

        if(health <= 0f)
        {
            damageAmount += health;
            health = 0f;
            OnDie.Invoke(damageAmount);
            return damageAmount;
        }

        OnHit.Invoke(damageAmount);
        return damageAmount;
    }
}
