using System.Collections;
using UnityEngine;

public class FuelRoom : RoomSystem, IRoomAction
{
    [SerializeField] float _leakRate;
    bool _canLeak = true;
    Coroutine _leakCo;
    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.roomAction += RoomAction;
        //RoomManager.Instance.Rooms.Add(this);
    }

    public void RoomAction()
    {
        if (_isDamaged && _canLeak)
        {
            _canLeak = false;
            _leakCo = StartCoroutine(LeakCoroutine());
        }
    }

    IEnumerator LeakCoroutine()
    {
        GameManager.Truck.RemoveFuel();
        yield return new WaitForSeconds(_leakRate);
        _canLeak = true;
    }

    public void CrewLevelUp()
    {
        return;
    }
}
