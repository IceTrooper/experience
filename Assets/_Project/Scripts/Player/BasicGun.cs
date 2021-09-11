using UnityEngine;

public class BasicGun : Gun
{
    [SerializeField] private Transform riflePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    public override void Fire()
    {
        var bullet = Instantiate(bulletPrefab, riflePoint.position, riflePoint.rotation);
        var bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
