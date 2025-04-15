using TMPro;
using UnityEngine;

public class CombatEventUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _combatMessage;

    private float elapsed = 0f;
    private float blinkDuration = 2f; // 블링크 지속 시간
    private void Start()
    {
        _combatMessage.text = "전투 발생!";
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        float alpha = Mathf.PingPong(Time.time * 1f, 0.5f) + 0.5f; // 0~1 반복
        _combatMessage.alpha = alpha;
    }
}
