using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HireEventUI : MonoBehaviour
{
    [Header("Crew 정보")]
    [SerializeField] private GameObject _crewIllustration;
    [SerializeField] private TextMeshProUGUI _name;
    //[SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private TextMeshProUGUI _crewDescription;

    [SerializeField] private TextMeshProUGUI _maxHealth;
    [SerializeField] private TextMeshProUGUI _moveSpeed;
    [SerializeField] private TextMeshProUGUI _repairSpeed;
    [SerializeField] private TextMeshProUGUI _attackSpeed;
    [SerializeField] private TextMeshProUGUI _healSpeed;
    [SerializeField] private TextMeshProUGUI _avoidance;
    [SerializeField] private TextMeshProUGUI _evilRate;

    [Header("추가적인 이벤트 UI")]
    [SerializeField] private TextMeshProUGUI _eventTitle;
    [SerializeField] private TextMeshProUGUI _eventDescription;

    private HireEventResultDto _dto;

    public void GetUIInfomation(HireEventResultDto dto)
    {
        _dto = dto;
        ShowUIInfomation();
    }

    public void OnClickHireButton()
    {
        //GameManager.CrewManager.TakeCrew(_dto.crewKey);
        GameManager.CrewManager.Spawn(_dto.crewKey);
        Destroy(gameObject);
    }

    public void OnClickCloseButton()
    {
        Destroy(gameObject);
    }

    private void ShowUIInfomation()
    {
        SetCrewInformation();
        SetEventDescription();
    }

    private void SetCrewInformation()
    {
        CrewSO crew = GameManager.Data.crewData[_dto.crewKey];
        _crewIllustration.GetComponent<Image>().sprite = crew.Illustration;
        _name.text = crew.Name;
        //_rank.text = crew.Rank.ToString();
        _crewDescription.text = crew.Description;
        _maxHealth.text = crew.MaxHealth.ToString();
        _moveSpeed.text = crew.MoveSpeed.ToString();
        _repairSpeed.text = crew.RepairSpeed.ToString();
        _attackSpeed.text = crew.AttackSpeed.ToString();
        _healSpeed.text = crew.HealSpeed.ToString();
        _avoidance.text = crew.Avoidance.ToString();
        _evilRate.text = crew.EvilRate.ToString();
    }

    private void SetEventDescription()
    {
        _eventTitle.text = "Hire Event";
        _eventDescription.text = _dto.description;
    }
}
