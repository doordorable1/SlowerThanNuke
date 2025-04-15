using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class MapManager : MonoBehaviour
{
    public List<NodeButton> allNodeButtons;
    public NodeDataSO startNode;
    [SerializeField] NodeDataSO _currentNode;
    public Canvas _canvas;

    private void Start()
    {
        _canvas.enabled = false;
        _currentNode = startNode;
        UpdateInteractableNodes();
    }

    public void MapToggle()
    {
        if(_canvas.enabled)
        {
            this._canvas.enabled = false;
        }
        else
        {
            this._canvas.enabled = true;
        }
    }

    // OnClick() invoked on Inspector 
    public void SelectNode(NodeButton selectedButton)
    {
        if(GameManager.Instance.CurrentState == State.Fight || GameManager.Instance.CurrentState == State.BossFight)
        {
            return;
        }

        GameManager.Truck.RemoveFuel();
        Button buttonComp = selectedButton.GetComponent<Button>();

        if (buttonComp != null && !buttonComp.interactable)
        {
            return;
        }

        selectedButton.wasClicked = true;

        if (selectedButton.nodeData != null)
        {
            Debug.Log($"Node selected : {selectedButton.nodeData.nodeType}");
            _currentNode = selectedButton.nodeData;
            UpdateInteractableNodes();

            NodeEvent nodeEvent = null;
            switch (_currentNode.nodeType)
            {
                case EventType.CrewFight:
                    nodeEvent = new GameObject("FightEvent").AddComponent<FightEvent>();
                    MapToggle();
                    break;
                case EventType.Repair:
                    nodeEvent = new GameObject("RepairEvent").AddComponent<RepairEvent>();
                    MapToggle();
                    break;
                case EventType.Fuel:
                    nodeEvent = new GameObject("FuelEvent").AddComponent<FuelEvent>();
                    MapToggle();
                    break;
                case EventType.Hire:
                    nodeEvent = new GameObject("HireEvent").AddComponent<HireEvent>();
                    MapToggle();
                    break;
                case EventType.Combat:
                    nodeEvent = new GameObject("CombatEvent").AddComponent<CombatEvent>();
                    nodeEvent.SetEventType(EventType.Combat);
                    MapToggle();
                    break;
                case EventType.BossCombat:
                    nodeEvent = new GameObject("CombatEvent").AddComponent<CombatEvent>();
                    nodeEvent.SetEventType(EventType.BossCombat);
                    MapToggle();
                    break;
            }

            // Optional: Handle the created event if needed
            if (nodeEvent != null)
            {
                // You might want to show the event or do something with it
                nodeEvent.ShowEventCanvas();
            }
        }
    }

    public void UpdateInteractableNodes()
    {
        List<NodeDataSO> reachableData = new List<NodeDataSO>();
        reachableData = _currentNode.nextNodes;
        foreach (NodeButton buttonInstance in allNodeButtons)
        {
            if (buttonInstance == null) continue;
            bool shouldBeInteractable = false;
            if (_currentNode == null)
            {
                if (startNode != null && buttonInstance.nodeData == startNode)
                {
                    shouldBeInteractable = true;
                }
            }
            else
            { 
                if (reachableData.Contains(buttonInstance.nodeData))
                {
                    shouldBeInteractable = true;
                }
            }
            buttonInstance.SetInteractable(shouldBeInteractable);
        }
    }
}
