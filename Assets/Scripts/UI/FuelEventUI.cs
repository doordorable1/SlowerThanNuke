using TMPro;
using UnityEngine;

public class FuelEventUI : MonoBehaviour
{
    private FuelEventResultDto _dto;

    [SerializeField] private TextMeshProUGUI _eventTitle;
    [SerializeField] private TextMeshProUGUI _description;

    public void GetUIInfomation(FuelEventResultDto dto)
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
        _eventTitle.text = "¿¬·á È¹µæ!!";
        _description.text = _dto.description;
    }
}
