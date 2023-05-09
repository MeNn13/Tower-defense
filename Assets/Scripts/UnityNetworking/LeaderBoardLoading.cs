using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardLoading : MonoBehaviour
{
    [Header("Unity Setup Reference")]
    [SerializeField] private Transform scrollViewContext;
    [SerializeField] private GameObject userDataPref;

    private void Start()
    {
        StartCoroutine(SendRequest());
    }

    private IEnumerator SendRequest()
    {
        using UnityWebRequest request = UnityWebRequest.Get(URL.API_URL + "/user/leaderBoard");

        yield return request.SendWebRequest();

        string json = "{\"UsersData\":" + request.downloadHandler.text + "}";

        Response response = JsonUtility.FromJson<Response>(json);

        for (int i = 0; i < response.UsersData.Length; i++)
        {
            GameObject userBlock = Instantiate(userDataPref, scrollViewContext);
            userBlock.GetComponent<DataUserBlock>().ChangeUserData(response.UsersData[i].login,
                response.UsersData[i].score.ToString());
        }
    }
}
