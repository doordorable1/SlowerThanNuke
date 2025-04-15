using UnityEngine;

public abstract class NodeEvent : MonoBehaviour
{
    protected GameObject _eventCanvasPrefab;

    protected virtual void Start()
    {
        // 원래는 UIManager에서 관리해야하는 부분입니다.
        // 하지만 현재 다들 기능 구현먼저 하느라 UI가 각가의 스크립트에 다 들어가 있죠?
        // 그래서 나중에 한번에 UIManager 리팩토링 실시하고자 합니다.
        //LoadNodeEventPrefab();
    }

    protected virtual void LoadNodeEventPrefab()
    {
        _eventCanvasPrefab = Resources.Load<GameObject>("Event/EventCanvas");
        if (_eventCanvasPrefab != null) { print("first of all Not is null"); }
        else { print("null!!!!!!!!!!!!"); }
    }

    public virtual void ShowEventCanvas()
    {
        //GameManager.Map.UpdateInteractableNodes();
        LoadNodeEventPrefab();
        Instantiate(_eventCanvasPrefab);
    }

    public virtual void SetEventType(EventType eventType)
    {
        // Do nothing
    }
}
