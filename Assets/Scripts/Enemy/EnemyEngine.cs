using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    EnemyController enemy;

    private void Start()
    {
        enemy = transform.parent.GetComponent<EnemyController>();
    }

    public void TakeDamage(float damageAmount)
    {
        enemy.TakeDamage(damageAmount);
    }
}
