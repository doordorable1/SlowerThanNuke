using UnityEngine;

public class HireEvent : NodeEvent
{
    private HireEventUI _hireEventUI;
    private HireEventResultDto _hireEventResultDto = new HireEventResultDto();
    private GameObject copyPrefab;

    protected override void Start()
    {
    }

    // 이벤트에 연결될 메서드
    public override void ShowEventCanvas()
    {
        LoadNodeEventPrefab();
        copyPrefab = Instantiate(_eventCanvasPrefab);
        _hireEventUI = copyPrefab.GetComponent<HireEventUI>();
        SettingEventCanvas();
    }

    protected override void LoadNodeEventPrefab()
    {
        _eventCanvasPrefab = Resources.Load<GameObject>("EventCanvas/HireEventCanvas");
    }

    private void SettingEventCanvas()
    {
        SetHireCrew();
        SetDescription();
        _hireEventUI.GetUIInfomation(_hireEventResultDto);
    }

    private void SetHireCrew()
    {
        // 더 복잡한 로직이 들어갈 수도 있어서 모듈화하였습니다.
        _hireEventResultDto.crewKey = GameManager.CrewManager.ShowCrew();
    }

    private void SetDescription()
    {
        // 아래와 같이 구성할 시, 메서드 호출 순서를 지켜야합니다.
        _hireEventResultDto.description = $"{GameManager.Data.crewData[_hireEventResultDto.crewKey].Name}가 합류하고 싶어합니다.";
    }

}
