using TMPro;
using UnityEngine;

public class FightEventUI : MonoBehaviour
{
    private FightEventResultDto _dto;

    [SerializeField] private TextMeshProUGUI _eventTitle;
    [SerializeField] private TextMeshProUGUI _description;

    public void GetUIInfomation(FightEventResultDto dto)
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
        _eventTitle.text = "Fight!!";
        _description.text = _dto.description;
    }
}
