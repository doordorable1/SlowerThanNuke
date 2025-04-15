using UnityEngine;
using System.Collections.Generic;

public class TruckManager
{
    public List<Crew> CrewsInTruck => _crewsInTruck;
    public float Avoidance => _avoidance;
    public float TruckHealth => _truckHealth;
    int _fuel = 30;
    float _truckHealth = 1000;
    float _currentTruckHealth;
    float _avoidance;
    List<Crew> _crewsInTruck = new List<Crew>();

    public void Init()
    {
        _currentTruckHealth = _truckHealth;
    }

    public void SetMissChance(float avoidChance)
    {
        _avoidance = avoidChance;
    }

    public void RemoveFuel()
    {
        _fuel--;
        if (_fuel <= 0)
        {
            _fuel = 0;
            GameManager.Instance.GameOver(1);
        }
        GameManager.UI.UI_Canvas.UpdateOil(_fuel);
    }
    public void AddFuel(int fuelAmount)
    {
        _fuel += fuelAmount;
        if (_fuel > 100)
        {
            _fuel = 100;
        }
        GameManager.UI.UI_Canvas.UpdateOil(_fuel);
    }
    public void RepairTruck(float repairAmount)
    {
        _currentTruckHealth += repairAmount;
        if (_currentTruckHealth >= _truckHealth)
        {
            _currentTruckHealth = _truckHealth;
        }
        GameManager.UI.UI_Canvas.UpdateHP(_currentTruckHealth);
    }

    public void DamageTruck(float damageAmount)
    {
        _currentTruckHealth -= damageAmount;
        Debug.Log(_currentTruckHealth.ToString());
        GameManager.UI.UI_Canvas.UpdateHP(_currentTruckHealth);
        if (_currentTruckHealth <= 0)
        {
            _currentTruckHealth = 0;
            GameManager.UI.UI_Canvas.UpdateHP(_currentTruckHealth);
            Debug.Log("Truck is gone");
            GameManager.Instance.ChangeState(State.Idle);
            GameManager.Instance.GameOver(2);
        }
    }
}