using TMPro;
using UnityEngine;

public class DataUserBlock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loginText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void ChangeUserData(string login, string score)
    {
        loginText.text = login;
        scoreText.text = score;
    }
}
