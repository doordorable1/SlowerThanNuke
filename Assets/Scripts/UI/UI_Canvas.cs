using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Canvas : MonoBehaviour
{
    [SerializeField] Image[] _weaponIcons;
    [SerializeField] UI_TruckHPSlider TruckHPbar;
    [SerializeField] TextMeshProUGUI oilCount;
    [SerializeField] GameObject pauseImage;

    List<GameObject> slotObj;
    List<string> slotCrewName;


    [SerializeField] Transform slotParents;
    int MaximumCrewCount = 6;

    [SerializeField] GameObject slotPrefab;
    void Awake()
    {
        slotObj = new List<GameObject>(); 
        slotCrewName = new List<string>();
    }

    private void Start()
    {
        TruckHPbar.SetMax(GameManager.Truck.TruckHealth);
    }
    public void InstantiateCrewSlot(string crewName)
    {
        int slotNum = slotObj.Count;
        if (slotNum >= MaximumCrewCount)
        {
            Debug.LogError("Fail_ InstantateCrewSlot() : IsSlot is Full");
            return;
        }
        GameObject slotObject = Instantiate(slotPrefab, slotParents);
        slotObj.Add(slotObject);
        slotCrewName.Add(crewName);
        slotObj[slotNum].GetComponent<UI_Slot>().InstantiateCrew(crewName);
    }
    public void UpdateHP(string crewName, float crewHP)
    {
        slotObj[slotCrewName.IndexOf(crewName)].GetComponent<UI_Slot>().UpdateHP(crewHP);
    }
    public void UpdateHP(float HP)
    {
        TruckHPbar.UpdateHP(HP);
    }
    public void RemoveSlot(string crewName)
    {
        Destroy(slotObj[slotCrewName.IndexOf(crewName)]);
        slotObj.RemoveAt(slotCrewName.IndexOf(crewName));
        slotCrewName.Remove(crewName);
    }
    public void UpdateWeaponSlot(Sprite weaponIcon, int slotNum)
    {
        _weaponIcons[slotNum].sprite = weaponIcon;
    }
    public void UpdateOil(int count)
    {
        oilCount.text = count.ToString();
    }
    public void PauseGame()
    {
        pauseImage.SetActive(!pauseImage.activeSelf);
    }
    public void SelectWeapon(int weapon)
    {
        if (weapon == 1)
        {
            _weaponIcons[weapon].color = new Color(100f / 255f, 255f / 255f, 150f / 255f);
            _weaponIcons[0].color = Color.white;
            return;
        }
        _weaponIcons[weapon].color = new Color(100f / 255f, 255f / 255f, 150f / 255f);
        _weaponIcons[1].color = Color.white;
    }
}
