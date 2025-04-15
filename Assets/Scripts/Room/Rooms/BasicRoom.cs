public class BasicRoom : RoomSystem, IRoomAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Init()
    {
        base.Init();
        //RoomManager.Instance.Rooms.Add(this);
    }

    public void RoomAction()
    {
    }

    public void CrewLevelUp()
    {
    }
}
