using UnityEngine;

public class SlowingTower : MonoBehaviour
{
    [SerializeField] private float slowingForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.Speed /= slowingForce;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Enemy>().Speed *= slowingForce;
    }
}
