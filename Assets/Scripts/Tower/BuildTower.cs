using System;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public static Func<int, bool> OnBuildTower;

    [SerializeField] private GameObject placeUI;
    [SerializeField] private Transform spawnTower;

    private bool _isActive = false;

    private void OnMouseDown()
    {
        Active();
    }

    public void TowerType(TowerData tower)
    {
        if (OnBuildTower.Invoke(tower.Price))
        {
            Instantiate(tower.TowerPref, spawnTower.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void Active()
    {
        if (_isActive)
        {
            placeUI.SetActive(false);
            _isActive = false;
        }
        else
        {
            placeUI.SetActive(true);
            _isActive = true;
        }
    }
}
