using System.Collections.Generic;

public class EngineRoom : RoomSystem, IRoomAction
{
    float _baseAvoidance;

    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.roomAction += RoomAction;
        RoomManager.Instance.crewLevelUpAction += CrewLevelUp;
        //RoomManager.Instance.Rooms.Add(this);
    }

    public void RoomAction()
    {
        if (!_isDamaged)
        {
            float additionalAvoidance = 0;
            List<Crew> copy = new List<Crew>(CrewsInRoom);
            foreach (Crew crew in copy)
            {
                additionalAvoidance += crew.GetCrewStat(2);
            }
            GameManager.Truck.SetMissChance(_baseAvoidance + additionalAvoidance);
        }
    }

    public void CrewLevelUp()
    {
        if (GameManager.Instance.CurrentState == State.Fight || GameManager.Instance.CurrentState == State.BossFight)
        {
            List<Crew> copy = new List<Crew>(CrewsInRoom);
            foreach (Crew crew in copy)
            {
                crew.UpdateAdditionalStats(3);
            }
        }
    }
}
