using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SignIn : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private TMP_InputField loginField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject authBlock;

    [Header("Unity Setup Reference")]
    [SerializeField] private string apiUrl;

    private void Awake()
    {
        URL.API_URL = apiUrl;
    }

    private void Start()
    {
        if (UserData.token != null)
            authBlock.SetActive(false);

        scoreText.text = "Ваш счет : " + UserData.score.ToString();
    }

    public void Auth()
    {
        StartCoroutine(SendRequest());
    }

    private IEnumerator SendRequest()
    {
        string url = $"{apiUrl}/SignIn?login={loginField.text}&password={passwordField.text}";

        using UnityWebRequest request = UnityWebRequest.Post(url, "POST");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }

        Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

        if (response.token != null)
        {
            UserData.token = response.token;
            UserData.login = response.login;
            StartCoroutine(GetScore());
            authBlock.SetActive(false);
        }
        else
        {
            Debug.Log("Неправильно введен логин или пароль");
        }
    }

    private IEnumerator GetScore()
    {
        string url = $"{URL.API_URL}/user/getScore/?login={UserData.login}";

        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

        UserData.score = response.score;

        scoreText.text = "Ваш счет : " + UserData.score.ToString();       
    }
}
