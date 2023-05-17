using UnityEngine;
using UnityEngine.UI;

public class LevelPassed : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    void Start()
    {
        int levelPassed = PlayerPrefs.GetInt("Level");

        levelButtons[levelPassed].interactable = true;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelPassed - i != 0)
                levelButtons[levelPassed - i].interactable = true;
        }
    }
}
