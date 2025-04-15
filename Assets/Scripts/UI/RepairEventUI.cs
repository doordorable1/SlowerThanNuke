using TMPro;
using UnityEngine;

public class RepairEventUI : MonoBehaviour
{
    private RepairEventResultDto _dto;

    [SerializeField] private TextMeshProUGUI _eventTitle;
    [SerializeField] private TextMeshProUGUI _description;

    public void GetUIInfomation(RepairEventResultDto dto)
    {
        _dto = dto;
        ShowUIInformation();
    }

    public void OnClickCloseButton()
    {
        Destroy(gameObject);
    }

    private void ShowUIInformation()
    {
        SetEventDescription();
    }

    private void SetEventDescription()
    {
        _eventTitle.text = "Repair Chance";
        _description.text = _dto.description;
    }
}
