using UnityEngine;

public class CombatEvent : NodeEvent
{
    public EventType eventType;

    public override void ShowEventCanvas()
    {
        LoadNodeEventPrefab();
        GameObject combatCanvas = Instantiate(_eventCanvasPrefab);
        Destroy(combatCanvas, 3f);
        UpdateGameValues();
    }

    public override void SetEventType(EventType eventType)
    {
        this.eventType = eventType;
    }

    protected override void LoadNodeEventPrefab()
    {
        _eventCanvasPrefab = Resources.Load<GameObject>("EventCanvas/CombatEventCanvas");
    }

    private void UpdateGameValues()
    {
        if (eventType == EventType.BossCombat)
        {
            GameManager.Combat.SpawnBoss();
            GameManager.Instance.CurrentState = State.BossFight;
        }
        else if (eventType == EventType.Combat)
        {
            GameManager.Combat.SpawnEnemy();
            GameManager.Instance.CurrentState = State.Fight;
        }
    }
}
