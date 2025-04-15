using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _crewName;
    [SerializeField] Image _crewImage;
    [SerializeField] UI_HPbar _hpBar;
    public void InstantiateCrew(string crewName)
    {
        // ũ�� ������ so���� ��������
        CrewSO crew = GameManager.Data.GetCrewInfo(crewName);
        _crewName.text = crew.Name;
        _crewImage.sprite = crew.Illustration;
        _hpBar.SetMax(crew.MaxHealth);
    }
    public void UpdateHP(float crewHP)
    {
        _hpBar.UpdateHP(crewHP);
    }
    public void DestroySlot()
    {
        Destroy(gameObject);
    }
}
