using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float range = 15f;

    [Header("Unity Setup Field")]
    [SerializeField] private Transform tower;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform spawnBullet;

    [Header("UI")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button damageBtn;
    [SerializeField] private Button speedBtn;

    [SerializeField] private float cooldown = 1f;
    public float Cooldown
    {
        get { return cooldown; }
        set
        {
            if (value > cooldown && value < 5f)
                cooldown = value;
        }
    }

    private float fireCountDown = 0f;
    private Transform target;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(tower.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        tower.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / cooldown;
        }

        fireCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletRef = Instantiate(bulletPref, spawnBullet.position, spawnBullet.rotation);
        Bullet bullet = bulletRef.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnMouseDown()
    {
        upgradeUI.SetActive(true);
        Invoke("ButtonDelay", 0.5f);
    }

    private void ButtonDelay()
    {
        damageBtn.interactable = true;
        speedBtn.interactable = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
