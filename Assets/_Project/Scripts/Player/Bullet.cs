using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;

    private void OnEnable()
    {
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            var damagable = collision.gameObject.GetComponent<Damagable>();
            damagable.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
}
