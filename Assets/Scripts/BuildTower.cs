using System;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public static Func<int, bool> OnBuildTower;

    [SerializeField] private GameObject towerPref;
    [SerializeField] private Transform spawnTower;
    [SerializeField] private int price = 10;

    private void OnMouseDown()
    {
        if (OnBuildTower.Invoke(price))
        {
            GameObject towerClone = Instantiate(towerPref, spawnTower.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
