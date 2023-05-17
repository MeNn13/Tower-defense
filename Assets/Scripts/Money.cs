using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int money = 20;

    private TextMeshProUGUI moneyUI;

    private void OnEnable()
    {
        EventBus.OnEnemyDead += AddMoney;
        BuildTower.OnBuildTower += BuyTower;
        UpgradeTower.OnDamagePrice += DamageUpgrade;
        UpgradeTower.OnSpeedPrice += SpeedUpgrade;
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDead -= AddMoney;
        BuildTower.OnBuildTower -= BuyTower; 
        UpgradeTower.OnDamagePrice -= DamageUpgrade;
        UpgradeTower.OnSpeedPrice -= SpeedUpgrade;
    }

    private void Start()
    {
        moneyUI = GetComponent<TextMeshProUGUI>();
        moneyUI.text = $"$ {money}";
    }

    private void AddMoney(int cost)
    {
        money += cost;
        moneyUI.text = $"$ {money}";
    }

    private bool BuyTower (int price)
    {
        if (money < price)
        {
            return false;
        }
        else
        {
            money -= price;
            moneyUI.text = $"$ {money}";
            return true;
        }       
    }

    private bool SpeedUpgrade(int price)
    {
        if (money < price)
        {
            return false;
        }
        else
        {
            money -= price;
            moneyUI.text = $"$ {money}";
            return true;
        }
    }

    private bool DamageUpgrade(int price)
    {
        if (money < price)
        {
            return false;
        }
        else
        {
            money -= price;
            moneyUI.text = $"$ {money}";
            return true;
        }
    }
}
