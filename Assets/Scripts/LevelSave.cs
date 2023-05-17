using UnityEngine;

public class LevelSave : MonoBehaviour
{
    public void SaveLevel(int level)
    {
        int levelPassed = PlayerPrefs.GetInt("Level");

        if (levelPassed != level)
            PlayerPrefs.SetInt("Level", level);
    }
}
