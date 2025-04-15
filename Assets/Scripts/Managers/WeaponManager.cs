using System.Collections.Generic;
using UnityEngine;

public class WeaponManager
{
    Dictionary<string, GameObject> _allWeapons = new Dictionary<string, GameObject>();//��� ������ ����
    Dictionary<string, bool> _hasWeapon = new Dictionary<string, bool>(); //������ ���� ��ųʸ�
    WeaponRoom[] weaponRooms;
    bool _isSlot1 = true;
    public void Init()
    {
        LoadAllWeapons();
        weaponRooms = Object.FindObjectsByType<WeaponRoom>(FindObjectsSortMode.InstanceID);
        Debug.Log(weaponRooms[0].name);
        Debug.Log(weaponRooms[1].name);
        GameManager.Input.setTargetAction += SetTarget;
        GameManager.Input.selectSlotAction += ChangeSlot;
        GameManager.UI.UI_Canvas.SelectWeapon(0);

    }
    void ChangeSlot(bool slot)
    {
        _isSlot1 = slot;
        if (_isSlot1)
        {
            GameManager.UI.UI_Canvas.SelectWeapon(0);
        } else
        {
            GameManager.UI.UI_Canvas.SelectWeapon(1);
        }
        
    }

    //���� ������ �̸����� ��� ���� ������ �ε�
    //��� ������ �ʱ� ���´� �������� �������� ����
    void LoadAllWeapons()
    {
        GameObject[] allWeapons = Resources.LoadAll<GameObject>("Weapons");
        foreach (GameObject weapon in allWeapons)
        {
            _allWeapons.Add(weapon.name, weapon);
            _hasWeapon.Add(weapon.name, false);
        }
    }
    public GameObject GetWeapon(string weaponName)
    {
        if (!_hasWeapon[weaponName])
        {
            _hasWeapon[weaponName] = true;
            return _allWeapons[weaponName];
        }
        else
        {
            Debug.LogWarning("Invalid code or you already have that weapon.");
            return null;
        }
    }

    public GameObject GetRandomWeapon()
    {
        List<string> availableWeapons = new List<string>();
        foreach (string key in _hasWeapon.Keys)
        {
            if (!_hasWeapon[key])
            {
                availableWeapons.Add(key);
            }
        }
        int rand = Random.Range(0, availableWeapons.Count);
        return GetWeapon(availableWeapons[rand]);
    }

    public void ReturnWeapon(string weaponName)
    {
        _hasWeapon[weaponName] = false;
    }

    public void SetWeapon(GameObject weapon, int slot)
    {
        GameObject weaponToSet = Object.Instantiate(weapon);
        GameManager.UI.UI_Canvas.UpdateWeaponSlot(weapon.GetComponent<Weapon>().Icon, slot);
        weaponToSet.name = weapon.name;
        weaponRooms[slot].SetWeapon(weaponToSet);
    }

    void SetTarget(GameObject target)
    {
        if (_isSlot1)
        {
            weaponRooms[0].SetTarget(target);
        } else
        {
            weaponRooms[1].SetTarget(target);
        }
    }
}
