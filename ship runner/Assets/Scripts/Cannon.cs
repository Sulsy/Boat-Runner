using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform firePoint; // точка, откуда будет вылетать снаряд
    public GameObject bulletPrefab; // префаб снаряда
    public float damage = 10f; // урон снаряда
    public float reloadTime = 1f;
    public float fireRate = 1f; // время перезарядки
    public int magazineSize = 5; // размер магазина
    public float bulletSpeed = 10f; // скорость полета снаряда
    public int type;
    public int price;
    protected int currentAmmo; // текущее количество патронов в магазине
    public float nextFireTime; // время следующего выстрела

    protected bool isReloading = false; // флаг перезарядки

    void Start()
    {
        currentAmmo = magazineSize;
    }

    private bool IsReloading()
    {
        if (isReloading)
            return true;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return true;
        }

        return false;
    }

    protected IEnumerator<WaitForSeconds> Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }

    public void Shoot()
    {
        if (IsReloading()) return;
        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.tag = tag;
        bullet.GetComponent<Bullet>().Damage=damage;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody component is missing on the bullet prefab or it's child!");
        }
        Destroy(bullet, 2f); // Уничтожить снаряд через 2 секунды
    }
}
