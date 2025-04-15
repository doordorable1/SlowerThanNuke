using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour, IWeapon
{
    public Sprite Icon => _icon;

    bool isReloading = false;
    [SerializeField] Sprite _icon;
    [SerializeField] Transform _shootPos;
    [SerializeField]GameObject _bullet;
    [SerializeField] GameObject _shootEffect;
    [SerializeField]float defaultDelay = 3;
    [SerializeField]float bulletSpeed = 1f;
    [SerializeField]string shootSoundKey = "GunShot";
    public void Shoot(float shotDelay, Vector3 targetPos)
    {
        if (isReloading) { return; }
        GunSetRotation(targetPos);
        isReloading = true;
        StartCoroutine(ShootCor(shotDelay, targetPos));
    }
    void GunSetRotation(Vector3 targetPos)
    {
        Vector2 toTarget = targetPos - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, toTarget);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    IEnumerator ShootCor(float shotDelay, Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;

        Instantiate(_shootEffect, _shootPos.position, transform.rotation);
        GameObject bullet = Instantiate(_bullet, _shootPos.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        float reloadTime = defaultDelay * (1 - shotDelay / 100);

        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}


