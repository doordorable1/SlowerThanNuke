using UnityEngine;

[CreateAssetMenu(fileName = "CrewSO", menuName = "Scriptable Objects/CrewSO")]
public class CrewSO : ScriptableObject
{
    [Header("유닛 설정")]
    [SerializeField] private Sprite _illustration;
    [SerializeField] private string _name;
    [SerializeField] private CrewRank _rank;
    [SerializeField] private string _description;

    [Header("초기 스탯")]
    [Tooltip("최대체력")]
    [SerializeField] private float _maxHealth;
    [Tooltip("이동속도")]
    [SerializeField] private float _moveSpeed;
    [Tooltip("수리게이지 한번에 깍는 양")]
    [SerializeField] private float _repairSpeed;
    [Tooltip("무기기본 공격속도에서의 감소율")]
    [SerializeField] private float _attackSpeed;
    [Tooltip("한번 치료시 치료되는 양")]
    [SerializeField] private float _healSpeed;
    [Tooltip("추가 회피율")]
    [SerializeField] private float _avoidance; 

    [Range(0,100)]
    [Tooltip("성향")]
    [SerializeField] private float _evilRate;
    [Tooltip("남자 여부")]
    [SerializeField] bool _isMale;

    public Sprite Illustration => _illustration;
    public string Name => _name;
    public CrewRank Rank => _rank;
    public string Description => _description;
    public float MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float RepairSpeed => _repairSpeed;
    public float AttackSpeed => _attackSpeed;
    public float HealSpeed => _healSpeed;
    public float Avoidance => _avoidance;
    public float EvilRate => _evilRate;
    public bool IsMale => _isMale;
}
