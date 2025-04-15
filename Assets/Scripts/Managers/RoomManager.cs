using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }
    public float DamageChance => _damageChance;
    public float RepairLevelTime => _repairLevelTime;
    public float ContaminationTime => _contaminationTime;
    public float ContaminationDamage => _contaminationDamage;


    public Action roomAction;
    public Action crewLevelUpAction;

    [SerializeField] SpriteRenderer selectCellEffect;
    public GameObject contaminationEffect;
    public Cell SelectedCell { get; private set; }

    //public List<IRoomAction> Rooms { get; private set; } = new List<IRoomAction>();
    public List<Cell> Cells { get; private set; }

    // 캐릭터 능력치 증가
    bool _canLevelUp = true;
    Coroutine _levelUpCo;
    [SerializeField] float _levelUpSpeed;

    //피격 시 오염효과 관련 변수
    [SerializeField] float _damageChance = 25;
    [SerializeField] float _repairLevelTime = 1.5f;
    [SerializeField] float _contaminationTime = 1;
    [SerializeField] float _contaminationDamage = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Cells = new List<Cell>(FindObjectsByType<Cell>(FindObjectsSortMode.None));
    }
    void Start()
    {
        GameManager.Input.selectCellAction += SelectCell;
        GameManager.Input.deselectCellAction += DeselectCell;
    }

    private void Update()
    {
        roomAction.Invoke();
        if (_canLevelUp)
        {
            _canLevelUp = false;
            crewLevelUpAction.Invoke();
            _levelUpCo = StartCoroutine(crewLevelUpCoroutine());
        }
        //foreach (IRoomAction room in Rooms)
        //{
        //    room.RoomAction();
        //    if (_canLevelUp)
        //    {
        //        _canLevelUp = false;
        //        room.CrewLevelUp();
        //    }
        //}
        
    }

    public Cell FindEmptyCell()
    {
        foreach (Cell cell in Cells)
        {
            if (cell.CrewInCell == null)
            {
                return cell;
            }
        }

        Debug.Log("All cells are full");
        return null;
    }

    void SelectCell(GameObject cellToMove)
    {
        SelectedCell = cellToMove.GetComponent<Cell>();
        selectCellEffect.transform.position = cellToMove.transform.position;
        selectCellEffect.enabled = true;
        CrewController.Instance.SelectedCrew.Move(SelectedCell.transform.position);
        CrewController.Instance.Deselect();
    }

    public void DeselectCell()
    {
        SelectedCell = null;
        selectCellEffect.enabled = false;
    }

    IEnumerator crewLevelUpCoroutine()
    {
        yield return new WaitForSeconds(_levelUpSpeed);
        _canLevelUp = true;
    }
}
