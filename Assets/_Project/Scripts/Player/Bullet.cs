using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    private Rigidbody rb;

    private void OnEnable()
    {
        if(rb == null) rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(ProjectConstants.EnemyTag))
        {
            var damagable = collision.gameObject.GetComponent<Damagable>();
            damagable.TakeDamage(damageAmount);
        }

        gameObject.SetActive(false);
    }
}
