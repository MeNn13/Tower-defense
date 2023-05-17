using UnityEngine;

[CreateAssetMenu(fileName = "New TowerData", menuName = "Tower Data", order = 51)]
public class TowerData : ScriptableObject
{
    [SerializeField] private GameObject towerPref;
    public GameObject TowerPref { get => towerPref; }

    [SerializeField] private Sprite icons;
    public Sprite Icons { get => icons; }

    [SerializeField] private string towerName;
    public string TowerName { get => towerName; }

    [SerializeField] private int damage;
    public int Damage { get => damage; }

    [SerializeField] private int range;
    public int Range { get => range;}

    [SerializeField] private float cooldown;
    public float Cooldown { get => cooldown; }

    [SerializeField] private float turnSpeed;
    public float TurnSpeed { get => turnSpeed;}

    [SerializeField] private string description;
    public string Description { get => description; }

    [SerializeField] private int price;
    public int Price { get => price; }
}   
