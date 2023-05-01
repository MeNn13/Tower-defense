using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 10f;

    [Header("Unity Setup Field")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] private GameObject gameOverUI;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(1);
    }

    private void TakeDamage(float damageForce)
    {
        currentHealth -= damageForce;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}
