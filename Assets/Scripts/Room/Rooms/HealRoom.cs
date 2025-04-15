using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealRoom : RoomSystem, IRoomAction
{
    bool _canHeal = true;
    Coroutine _healCo;
    [SerializeField] float _healTime;

    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.roomAction += RoomAction;
        RoomManager.Instance.crewLevelUpAction += CrewLevelUp;
        //RoomManager.Instance.Rooms.Add(this);
    }
    public void RoomAction()
    {
        if (_canHeal && CrewsInRoom.Count > 0 && !_isDamaged)
        {
            _canHeal = false;
            _healCo = StartCoroutine(HealCoroutine());
        }

    }

    IEnumerator HealCoroutine()
    {
        List<Crew> copy = new List<Crew>(CrewsInRoom);
        foreach (Crew crew in copy)
        {
            Debug.Log("Ä¡·á Áß");
            crew.Heal();
        }
        yield return new WaitForSeconds(_healTime);
        _canHeal = true;
    }

    public void CrewLevelUp()
    {
        List<Crew> copy = new List<Crew>(CrewsInRoom);
        foreach (Crew crew in copy)
        {
            if (!crew.CheckFullHealth())
            {
                Debug.Log("Heal Room Level Up");
                crew.UpdateAdditionalStats(2);
            }
        }
    }
}
