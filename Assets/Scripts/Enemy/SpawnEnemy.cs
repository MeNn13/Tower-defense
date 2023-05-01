using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject enemyPref;

    [Header("Settings")]
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int amountWave = 1;

    private int amountEnemy = 5;

    private void Start()
    {
        StartCoroutine(SpawnEnemies(amountWave));
    }

    private IEnumerator SpawnEnemies(int _amountWave)
    {
        for (int i = 0; i < _amountWave; i++)
        {
            for (int j = 0; j < amountEnemy; j++)
            {
                yield return new WaitForSeconds(spawnRate);
                Instantiate(enemyPref, startPoint.transform.position, Quaternion.identity);
            }

            amountEnemy *= 2;
            yield return new WaitForSeconds(2);
        }
            
    }
}
