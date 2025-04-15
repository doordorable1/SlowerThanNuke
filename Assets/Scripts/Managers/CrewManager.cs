using System.Collections.Generic;
using UnityEngine;

public class CrewManager
{
    [SerializeField] int maxCrewCount = 6;

    List<string> _crewRankAPool;
    List<string> _crewRankBPool;
    List<string> _crewRankCPool;
    List<string> _crewListTruck;
    public List<string> CrewListTruck => _crewListTruck;

    public void Init()
    {
        Start();
    }
    void Start()
    {
        _crewRankAPool = new List<string>();
        _crewRankBPool = new List<string>();
        _crewRankCPool = new List<string>();
        _crewListTruck = new List<string>();
        CrewListReset();

    }
    void CrewListReset()
    {
        _crewRankAPool.Clear();
        _crewRankBPool.Clear();
        _crewRankCPool.Clear();
        _crewListTruck.Clear();
        _crewRankAPool = GameManager.Data.GetAllCrewCodeByRank(CrewRank.A);
        _crewRankBPool = GameManager.Data.GetAllCrewCodeByRank(CrewRank.B);
        _crewRankCPool = GameManager.Data.GetAllCrewCodeByRank(CrewRank.C);
    }
    public string ShowCrew() //랜덤 크루 보여주기
    {
        return ChoiceRank();
    }
    public void TakeCrew(string crewCode) // 크루 영입
    {
        if (_crewListTruck.Count >= maxCrewCount) return;

        _crewListTruck.Add(crewCode);
        GameManager.UI.UI_Canvas.InstantiateCrewSlot(crewCode);
            if      (_crewRankAPool.Contains(crewCode)) { _crewRankAPool.Remove(crewCode); }
            else if (_crewRankBPool.Contains(crewCode)) { _crewRankBPool.Remove(crewCode); }
            else                                        { _crewRankCPool.Remove(crewCode); }
    }
    string ChoiceRank()
    {
        float _rand = Random.Range(0f, 100f); // 0 이상 100 미만의 난수 생성
        int _class;

        if (_rand < 10f)
        {
            _class = 0;
        }
        else if (_rand < 35f)
        {
            _class = 1;
        }
        else
        {
            _class = 2;
        }

        switch (_class)
        {
            case 0:
                if (_crewRankAPool.Count == 0) { return ChoiceRank(); }
                return ChoiceCrew(_crewRankAPool);
            case 1:
                if (_crewRankBPool.Count == 0) { return ChoiceRank(); }
                return ChoiceCrew(_crewRankBPool);
            case 2:
                if (_crewRankCPool.Count == 0) { return ChoiceRank(); }
                return ChoiceCrew(_crewRankCPool);
            default:
                throw new System.Exception("Invalid crew class selected.");
        }
    }
    string ChoiceCrew(List<string> rankPool)
    {
        return rankPool[Random.Range(0, rankPool.Count)];
    }

    public void ExileCrew(string exileCrewCode) // 크루 방출
    {
        _crewListTruck.Remove(exileCrewCode);
        AddCrewInRankList(exileCrewCode);
        GameManager.UI.UI_Canvas.RemoveSlot(exileCrewCode);
        Debug.LogError("ExileCrew(CrewSO) -> not Contain TruckList");
    }
    void AddCrewInRankList(string crewCode)
    {
        switch (GameManager.Data.crewData[crewCode].Rank)
        {
            case CrewRank.A:
                _crewRankAPool.Add(crewCode);
                break;
            case CrewRank.B:
                _crewRankBPool.Add(crewCode);
                break;
            case CrewRank.C:
                _crewRankCPool.Add(crewCode);
                break;
        }
    }
    /// <summary>
    /// _takeCrew값은 ShowCrew()에서 가져온 값만 할당할 것
    /// </summary>
    /// <param name="_takeCrew"></param>
    /// <param name="_exileCrew"></param>
    public void ChangeCrew(string takeCrewCode, string exileCrewCode) // 크루 교체
    {
        ExileCrew(exileCrewCode);
        _crewListTruck.Add(takeCrewCode);
    }
    public void DeadCrew(string deadCrewCode) // 크루 사망
    {
        if (!_crewListTruck.Contains(deadCrewCode))
        {
            Debug.LogError("DeadCrew(CrewSO) -> not Contain TruckList");
        }
        _crewListTruck.Remove(deadCrewCode);
        GameManager.UI.UI_Canvas.RemoveSlot(deadCrewCode);
        GameManager.Data.crewIsAlive[deadCrewCode] = false;
    }

    public void Spawn(string crewCode)
    {
        Cell cellToSpawn = RoomManager.Instance.FindEmptyCell();
        TakeCrew(crewCode);
        GameObject crewToSpawn = null;
        if (GameManager.Data.GetCrewInfo(crewCode).IsMale)
        {
            crewToSpawn = Object.Instantiate(Resources.Load<GameObject>("CrewPrefab/Crew_M"));
        }
        else
        {
            crewToSpawn = Object.Instantiate(Resources.Load<GameObject>("CrewPrefab/Crew_F"));
        }
        crewToSpawn.GetComponent<Crew>().Init(crewCode, cellToSpawn);
        GameManager.Truck.CrewsInTruck.Add(crewToSpawn.GetComponent<Crew>());
    }
}
