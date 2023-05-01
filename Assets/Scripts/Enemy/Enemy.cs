using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 50f;

    [Header("Unity Setup Field")]
    [SerializeField] HealthBar healthBar;

    private Transform target;
    private int currentPointIndex = 0;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        target = Waypoints.points[currentPointIndex];
    }

    void Update()
    {
        Movement();
    }

    #region EnemyMovement
    private void Movement()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.1)
        {
            NextWaypoint();
        }
    }

    private void NextWaypoint()
    {
        if (currentPointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        currentPointIndex++;
        target = Waypoints.points[currentPointIndex];
    }
    #endregion

    public void TakeDamage(float damageForce)
    {
        currentHealth -= damageForce;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            EventBus.OnEnemyDead?.Invoke();
            Destroy(gameObject);
        }

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}
