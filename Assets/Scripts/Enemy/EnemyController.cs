using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public Action EnemyAttackAction;
    [SerializeField]float _engineHealth = 50;

    void Update()
    {
        if ((GameManager.Instance.CurrentState == State.Fight) || GameManager.Instance.CurrentState == State.BossFight)
        {
            EnemyAttackAction.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        _engineHealth -= damage;
        if (_engineHealth <= 0)
        {
            Debug.Log("Enemy Dead");
            Destroy(gameObject);
            GameManager.Combat.RemoveEnemy(this);
        }
    }
}
