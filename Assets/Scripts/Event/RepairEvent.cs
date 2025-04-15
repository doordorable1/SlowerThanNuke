using UnityEngine;

public class RepairEvent : NodeEvent
{
    private RepairEventUI _repairEventUI;
    private RepairEventResultDto _repairEventResultDto = new RepairEventResultDto();
    private GameObject copyPrefab;

    protected override void Start()
    {
    }

    public override void ShowEventCanvas()
    {
        LoadNodeEventPrefab();
        copyPrefab = Instantiate(_eventCanvasPrefab);
        _repairEventUI = copyPrefab.GetComponent<RepairEventUI>();
        SettingEventCanvas();
        UpdateGameValues();
    }

    protected override void LoadNodeEventPrefab()
    {
        _eventCanvasPrefab = Resources.Load<GameObject>("EventCanvas/RepairEventCanvas");
    }

    private void SettingEventCanvas()
    {
        SetRepairAmount();
        SetDescription();
        _repairEventUI.GetUIInfomation(_repairEventResultDto);
    }

    private void UpdateGameValues()
    {
        // 수리된 양만큼 차량의 체력을 회복합니다.
        GameManager.Truck.RepairTruck(_repairEventResultDto.repairAmount);
    }

    private void SetRepairAmount()
    {
        // 수리할 양을 랜덤으로 설정합니다.
        _repairEventResultDto.repairAmount = Random.Range(200, 400);
    }

    private void SetDescription()
    {
        // 아래와 같이 구성할 시, 메서드 호출 순서를 지켜야합니다.
        _repairEventResultDto.description = $"모래폭풍이 약탈자들의 동선을 방해합니다 \n 부지런한 우리의 크루들은 이 기회를 놓치지 않습니다. \n 차체가 {_repairEventResultDto.repairAmount}만큼 수리되었습니다.";
    }
}
