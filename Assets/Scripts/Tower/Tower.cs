using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("Unity Setup Field")]
    [SerializeField] private TowerData towerData;
    [SerializeField] private Transform tower;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private AudioClip shootClip;

    [Header("UI")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button damageBtn;
    [SerializeField] private Button speedBtn;

    private float cooldown = 1f;
    public float Cooldown
    {
        get { return cooldown; }
        set
        {
            if (value > cooldown && value < 5f)
                cooldown = value;
        }
    }

    private float damageForce = 5f;
    public float DamageForce
    {
        get { return damageForce; }
        set
        {
            if (value > damageForce)
                damageForce = value;
        }
    }

    private float fireCountDown = 0f;
    private Transform target;
    private AudioSource audioSource;

    private void Awake()
    {
        damageForce = towerData.Damage;
        cooldown = towerData.Cooldown;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        if (nearestEnemy != null && shortestDistance <= towerData.Range)
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
        Vector3 rotation = Quaternion.Lerp(tower.rotation, lookRotation, Time.deltaTime * towerData.TurnSpeed).eulerAngles;
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
        audioSource.PlayOneShot(shootClip);
        GameObject bulletRef = Instantiate(bulletPref, spawnBullet.position, spawnBullet.rotation);
        Bullet bullet = bulletRef.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Init(target, damageForce);
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
        Gizmos.DrawWireSphere(transform.position, towerData.Range);
    }
}
