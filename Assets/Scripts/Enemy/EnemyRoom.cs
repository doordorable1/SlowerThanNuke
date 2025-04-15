using System.Collections;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    EnemyController _enemy;

    [SerializeField] GameObject weapon;
    [SerializeField] GameObject BreakdownImage;
    [SerializeField] float _enemyRoomHealth = 50;
    Transform targetRoom;
    float _currentHealth;

    bool _isBroken = false;
    [SerializeField] float _repairTime = 3;
    Coroutine _repairCo;

    void Start()
    {
        _enemy = transform.parent.GetComponent<EnemyController>();
        _enemy.EnemyAttackAction += Attack;
        _currentHealth = _enemyRoomHealth;
    }
    void Attack()
    {
        if (!_isBroken)
        {
            //Debug.Log("Enemy Shoot");
            SetTarget();
            weapon.GetComponent<IWeapon>().Shoot(0, targetRoom.position);
        }
    }

    void SetTarget()
    {
        int rand = Random.Range(0, RoomManager.Instance.Cells.Count);
        targetRoom = RoomManager.Instance.Cells[rand].transform;
    }
    public void TakeDamage(float damageAmount)
    {      
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            Breakdown(true);
            _isBroken = true;
            _repairCo = StartCoroutine(RepairCoroutine(_repairTime));
        }
        _enemy.TakeDamage(damageAmount * 0.7f);
    }

    public void TakeNapalm(float time)
    {
        _isBroken = true;
        if (_repairCo != null)
        {
            StopCoroutine(_repairCo);
        }
        _repairCo = StartCoroutine(RepairCoroutine(time));
    }
    IEnumerator RepairCoroutine(float repairTime)
    {
        yield return new WaitForSeconds(repairTime);
        Breakdown(false);
        _isBroken = false;
        _currentHealth = _enemyRoomHealth;
    }
    void Breakdown(bool isbreakdown)
    {
        BreakdownImage.SetActive(isbreakdown);
    }
}
