using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IUpgradable
{
    public GameObject Owner;
    public float nextFireTime;
    public float fireRate = 1f;
    public int type;
    public int price;

    protected int currentAmmo;
    protected bool isReloading = false;

    [SerializeField] 
    private Transform firePoint;
    [SerializeField] 
    private GameObject bulletPrefab;
    [SerializeField] 
    private float damage = 10f;
    [SerializeField] 
    private float reloadTime = 1f;
    [SerializeField] 
    private int magazineSize = 5;
    [SerializeField]
    private float bulletSpeed = 10f;

    public float Price { get; }

    public float GetPrice()
    {
        return price;
    }

    public void Shoot()
    {
        if (IsReloading()) return;
        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bull = bullet.GetComponent<Bullet>();
        bull.Damage = damage;
        bull.Owner = Owner;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = transform.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody component is missing on the bullet prefab or it's child!");
        }

        Destroy(bullet, 2f);
    }

    private void Start()
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

    private IEnumerator<WaitForSeconds> Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }
}