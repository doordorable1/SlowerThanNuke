using UnityEngine;

public interface IRoomAction
{
    // 각 방 행동 함수
    public void RoomAction();
    
    // 각 방 캐릭터 능력치 증가 함수
    public void CrewLevelUp();
}
