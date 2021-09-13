using UnityEngine;

public class BasicGun : Gun
{
    [SerializeField] private Transform riflePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Pooler pooler;

    /// <summary>
    /// Fire a bullet from the riflePoint with bulletSpeed.
    /// </summary>
    public override void Fire()
    {
        GameObject bullet;
        if(pooler != null)
        {
            bullet = pooler.Spawn();
            bullet.transform.position = riflePoint.position;
            bullet.transform.rotation = riflePoint.rotation;
        }
        else
        {
            bullet = Instantiate(bulletPrefab, riflePoint.position, riflePoint.rotation);
        }
        var bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
