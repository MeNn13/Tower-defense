using System;
using TMPro;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    public static Func<int, bool> OnDamagePrice;
    public static Func<int, bool> OnSpeedPrice;

    [Header("Upgrade UI")]
    [SerializeField] private TextMeshProUGUI speedPriceText;
    [SerializeField] private TextMeshProUGUI damagePriceText;

    [Header("Attributes")]
    [SerializeField] private Tower tower;

    private byte maxLevel = 3;
    private int speedPrice = 15;
    private byte speedLevel = 1;

    private int damagePrice = 15;
    private byte damageLevel = 1;

    private void Update()
    {
        if (speedPriceText == null || damagePriceText == null)
            return;

        speedPriceText.text = $"Speed \n $ {speedPrice}";
        damagePriceText.text = $"Damage \n $ {damagePrice}";
    }

    public void SpeedUpgrade()
    {
        if (speedLevel < maxLevel)
        {
            if (OnSpeedPrice.Invoke(speedPrice))
            {
                speedLevel++;
                tower.Cooldown = tower.Cooldown * 2;
                speedPrice *= speedLevel;
                speedPriceText.text = $"Speed \n $ {speedPrice}";
            }
        }
    }

    public void DamageUpgrade()
    {
        if (damageLevel < maxLevel)
        {
            if (OnDamagePrice.Invoke(damagePrice))
            {
                damageLevel++;
                tower.DamageForce = (float)Math.Round(tower.DamageForce * 2, 1);
                damagePrice *= damageLevel;
                damagePriceText.text = $"Damage \n $ {damagePrice}";
            }
        }
    }
}
