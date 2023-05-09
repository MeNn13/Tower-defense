using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject enemyPref;

    [Header("Settings")]
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int amountWave = 1;
    
    private int score = 0;
    private int amountEnemy = 5;

    private void OnEnable()
    {
        EventBus.OnEnemyDead += ScoreUp;
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDead -= ScoreUp;
    }

    private void Start()
    {
        score = UserData.score;
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
            yield return new WaitForSeconds(3);
            StartCoroutine(SaveData());
            UserData.score = score;
        }

    }

    private IEnumerator SaveData()
    {
        string url = $"{URL.API_URL}/user/updateScore?login={UserData.login}&score={score}";

        using UnityWebRequest request = UnityWebRequest.Post(url,"PUT");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    private void ScoreUp()
    {
        score++;
    }
}
