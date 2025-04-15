using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Crew : MonoBehaviour
{
    public CrewSO CrewInfo => _crewInfo;

    CrewSO _crewInfo;
    public Cell currentCell;

    // 캐릭터 스탯
    [Tooltip("캐릭터 이름")][SerializeField] string _crewCode;
    public string CrewCode => _crewCode;
    float _maxHealth;
    float _currentHealthPoint;
    float _moveSpeed;
    float _repairSpeed;
    float _attackSpeed;
    float _healSpeed;
    float _avoidance;
    float _evilRate;

    // 캐릭터 추가 스탯
    float _additionalRepairSpeed = 0;
    float _additionalAttackSpeed = 0;
    float _additionalHealSpeed = 0;
    float _additionalAvoidance = 0;

    // 캐릭터 이동
    [Tooltip("캐릭터 선택 효과")][SerializeField] SpriteRenderer _glow;
    NavMeshAgent _agent;
    Coroutine _moveCo;

    // 캐릭터 애니메이션
    Animator _anim;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _agent.updatePosition = false;
        _anim = GetComponent<Animator>();
    }

    public void Init(string crewCode, Cell cell)
    {
        _crewCode = crewCode;
        currentCell = cell;
        currentCell.transform.parent.GetComponent<RoomSystem>().AddCrew(this);
        currentCell.CrewInCell = this;
        transform.position = new Vector3(currentCell.transform.position.x, currentCell.transform.position.y, -0.1f);
        _agent.Warp(transform.position);
        UpdateStats();
        _currentHealthPoint = _maxHealth;
        _agent.speed = _moveSpeed;
    }

    // 캐릭터 이동 시 호출 함수
    public void Move(Vector3 targetPos)
    {
        currentCell.transform.parent.GetComponent<RoomSystem>().RemoveCrew(this);
        currentCell.CrewInCell = null;
        currentCell = RoomManager.Instance.SelectedCell;
        currentCell.CrewInCell = this;
        currentCell.transform.parent.GetComponent<RoomSystem>().AddCrew(this);
        if (_moveCo != null)
        {
            StopCoroutine(_moveCo);
            _anim.SetBool("IsMoving", true);
            _moveCo = StartCoroutine(MoveCoroutine(targetPos));
        }
        else
        {
            _anim.SetBool("IsMoving", true);
            _moveCo = StartCoroutine(MoveCoroutine(targetPos));
        }
    }

    // Navmesh 이용 길 찾는 코루틴
    // targetPos로 길을 찾아 이동
    // targetPos에는 이동하는 셀의 위치
    IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        _agent.SetDestination(targetPos);
        Debug.Log($"{_crewCode} is moving");
        while (Vector2.Distance(transform.position, targetPos) > 0.01f)
        {
            Vector3 diff = _agent.nextPosition - transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.position = _agent.nextPosition;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }
        transform.position = new Vector3(targetPos.x, targetPos.y, -0.1f);
        transform.rotation = currentCell.transform.rotation;
        _anim.SetBool("IsMoving", false);
        RoomManager.Instance.DeselectCell();
    }

    // 캐릭터 정보 업데이트
    void UpdateStats()
    {

        _crewInfo = GameManager.Data.GetCrewInfo(_crewCode);
        _maxHealth = _crewInfo.MaxHealth;
        _moveSpeed = _crewInfo.MoveSpeed;
        _repairSpeed = _crewInfo.RepairSpeed;
        _attackSpeed = _crewInfo.AttackSpeed;
        _healSpeed = _crewInfo.HealSpeed;
        _avoidance = _crewInfo.Avoidance;
        _evilRate = _crewInfo.EvilRate;

    }

    public void Heal()
    {
        _currentHealthPoint += _healSpeed + _additionalHealSpeed;
        CheckFullHealth();
        GameManager.UI.UI_Canvas.UpdateHP(_crewCode, _currentHealthPoint);
    }

    public bool CheckFullHealth()
    {
        if (_currentHealthPoint >= _maxHealth)
        {
            _currentHealthPoint = _maxHealth;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Damage(float amount)
    {
        _currentHealthPoint -= amount;
        GameManager.UI.UI_Canvas.UpdateHP(_crewCode, _currentHealthPoint);
        if (_currentHealthPoint <= 0)
        {
            _currentHealthPoint = 0;
            Debug.Log($"{_crewCode} is dead");
            currentCell.transform.parent.GetComponent<RoomSystem>().RemoveCrew(this);
            currentCell.CrewInCell = null;
            currentCell = null;
            GameManager.CrewManager.DeadCrew(_crewCode);
            Destroy(gameObject);
        }
    }

    public void ToggleGlow(bool state)
    {
        _glow.enabled = state;
    }

    /// <summary>
    /// 캐릭터 능력치 증가\n
    /// 0: 수리속도
    /// 1: 공격 속도
    /// 2: 치료 속도
    /// 3: 회피율
    /// </summary>
    /// <param name="code"> 증가하는 능력치 코드 </param>
    public void UpdateAdditionalStats(int code)
    {
        switch (code)
        {
            case 0:
                _additionalRepairSpeed += 0.1f;
                if (_additionalRepairSpeed > 6f)
                {
                    _additionalRepairSpeed = 6;
                }
                Debug.Log("수리 속도 증가");
                break;
            case 1:
                _additionalAttackSpeed += 0.06f;
                if (_additionalAttackSpeed > 6f)
                {
                    _additionalAttackSpeed = 6;
                }
                    Debug.Log("공격 속도 레벨업");
                break;
            case 2:
                _additionalHealSpeed += 0.1f;
                if (_additionalHealSpeed > 6f)
                {
                    _additionalHealSpeed = 6;
                }
                Debug.Log("치료 속도 레벨업");
                break;
            case 3:
                _additionalAvoidance += 0.1f;
                if (_additionalAvoidance > 6f)
                {
                    _additionalAvoidance = 6;
                }
                Debug.Log("회피율 증가");
                break;
            default:
                Debug.LogWarning("Invalid Stat code");
                break;
        }
    }

    /// <summary>
    /// 캐릭터 기본 + 추가 능력치, 성향 가져오기\n
    /// 0: 수리속도
    /// 1: 공격 속도
    /// 2: 회피율
    /// 3: 성향
    /// </summary>
    /// <param name="code"> 가져올 능력치 코드 </param>
    public float GetCrewStat(int code)
    {
        switch (code)
        {
            case 0:
                return (_repairSpeed + _additionalRepairSpeed);
            case 1:
                return (_attackSpeed + _additionalAttackSpeed);
            case 2:
                return (_avoidance + _additionalAvoidance);
            case 3:
                return (_evilRate);
            default:
                Debug.LogWarning("Invalid Stat code");
                return -1;
        }
    }

    public float[] GetAdditionalStats()
    {
        float[] additionalStats = {_additionalAttackSpeed, _additionalRepairSpeed, _additionalHealSpeed, _additionalAvoidance};
        return additionalStats;
    }
}
