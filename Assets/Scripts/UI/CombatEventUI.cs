using TMPro;
using UnityEngine;

public class CombatEventUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _combatMessage;

    private float elapsed = 0f;
    private float blinkDuration = 2f; // ��ũ ���� �ð�
    private void Start()
    {
        _combatMessage.text = "���� �߻�!";
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        float alpha = Mathf.PingPong(Time.time * 1f, 0.5f) + 0.5f; // 0~1 �ݺ�
        _combatMessage.alpha = alpha;
    }
}
