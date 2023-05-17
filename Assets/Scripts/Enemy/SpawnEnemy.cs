using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject[] enemyPref;
    [SerializeField] private GameObject levelPassedUI;

    [Header("Settings")]
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int amountWave = 1;
    
    private int score = 0;
    private int amountEnemy = 15;

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
                Instantiate(enemyPref[i], startPoint.transform.position, Quaternion.identity);
            }

            spawnRate += 0.5f;
            yield return new WaitForSeconds(3);
            StartCoroutine(SaveData());
            UserData.score = score;
        }

        levelPassedUI.SetActive(true);
        Time.timeScale = 0;
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

    private void ScoreUp(int cost)
    {
        score += cost;
    }
}
