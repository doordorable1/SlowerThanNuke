using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystem : MonoBehaviour
{
    protected List<Crew> CrewsInRoom { get; private set; } = new List<Crew>();

    
    protected bool _isDamaged = false;
    float _repairGage = 0;
    GameObject _roomContamination;
    bool _canContaminationDamage = true;
    Coroutine _contaminationCo;
    Coroutine _repairLevelCo;
    bool _canRepairLevelUp = true;
    
    

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void Update()
    {
        if (_isDamaged)
        {
            List<Crew> copy = new List<Crew>(CrewsInRoom);
            foreach (Crew crew in copy)
            {
                if (_canContaminationDamage)
                {
                    _canContaminationDamage = false;
                    crew.Damage(RoomManager.Instance.ContaminationDamage);
                    _contaminationCo = StartCoroutine(ContaminationCoroutine());
                }
                if (Repair(crew.GetCrewStat(0)))
                {
                    break;
                }

            }
        }
    }

    public void AddCrew(Crew crew)
    {
        CrewsInRoom.Add(crew);
    }

    public void RemoveCrew(Crew crew)
    {
        CrewsInRoom.Remove(crew);
    }

    public void TakeHit(float damageAmount)
    {
        GameManager.Truck.DamageTruck(damageAmount);
        List<Crew> copy = new List<Crew>(CrewsInRoom);
        foreach (Crew crew in copy)
        {
            crew.Damage(damageAmount * 0.25f);
        }
        float rand = Random.Range(0, 100);
        if(rand < RoomManager.Instance.DamageChance && !_isDamaged)
        {
            _isDamaged = true;
            _repairGage = 50;
            _roomContamination = Instantiate(RoomManager.Instance.contaminationEffect, transform.position, transform.rotation);
        }
    }

    IEnumerator ContaminationCoroutine()
    {
        yield return new WaitForSeconds(RoomManager.Instance.ContaminationTime);
        _canContaminationDamage = true;
    }

    public bool Repair(float amount)
    {
        Debug.Log("is repairing");
        _repairGage -= amount * Time.deltaTime;
        if (_canRepairLevelUp)
        {
            _canRepairLevelUp = false;
            List<Crew> copy = new List<Crew>(CrewsInRoom);
            foreach(Crew crew in copy)
            {
                crew.UpdateAdditionalStats(0);
            }
            _repairLevelCo = StartCoroutine(ReapirCoroutine());
        }
        if(_repairGage <= 0)
        {
            _isDamaged = false;
            Destroy(_roomContamination);
            return true;
        } else
        {
            return false;
        }
    }

    IEnumerator ReapirCoroutine()
    {
        yield return new WaitForSeconds(RoomManager.Instance.RepairLevelTime);
        _canRepairLevelUp = true;
    }

}
