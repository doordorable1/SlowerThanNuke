using System.Collections.Generic;
using UnityEngine;

public class WeaponRoom : RoomSystem, IRoomAction
{
    //List<Transform> _target = new List<Transform>();
    Transform _target;
    [SerializeField] Transform weaponLocation;
    GameObject _weapon;

    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.roomAction += RoomAction;
        RoomManager.Instance.crewLevelUpAction += CrewLevelUp;
        //RoomManager.Instance.Rooms.Add(this);
    }

    public void RoomAction()
    {
        if ((GameManager.Instance.CurrentState == State.Fight|| GameManager.Instance.CurrentState == State.BossFight) && CrewsInRoom.Count > 0  && !_isDamaged)
        {
            float reducedCoolDown = 0;
            List<Crew> copy = new List<Crew>(CrewsInRoom);
            foreach (Crew crew in copy)
            {
                reducedCoolDown += crew.GetCrewStat(1);
            }
            if (_target)
            {
                _weapon.GetComponent<IWeapon>().Shoot(reducedCoolDown, _target.position);
            }
            
        }
    }

    public void CrewLevelUp()
    {
        if ((GameManager.Instance.CurrentState == State.Fight || GameManager.Instance.CurrentState == State.BossFight) && !_isDamaged)
        {
            List<Crew> copy = new List<Crew>(CrewsInRoom);
            foreach (Crew crew in copy)
            {
                crew.UpdateAdditionalStats(1);
            }
        }
    }
    public void SetTarget(GameObject targetObject)
    {
        _target = targetObject.transform;
    }

    public void SetWeapon(GameObject weaponObject)
    {
       if(_weapon == null)
        {
            _weapon = weaponObject;
            _weapon.transform.position = transform.position;
        } else
        {
            GameManager.WeaponManager.ReturnWeapon(_weapon.name);
            Destroy(_weapon);
            _weapon = weaponObject;
            _weapon.transform.position = transform.position;
        }
    }

    public void DeselectTarget()
    {
        _target = null;
    }
}
