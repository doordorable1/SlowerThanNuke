using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeDataSO", menuName = "Scriptable Objects/NodeDataSO")]
public class NodeDataSO : ScriptableObject
{
    public EventType nodeType;
    public List<NodeDataSO> nextNodes;

    public NodeEvent CreateNodeEvent()
    {
        NodeEvent nodeEvent = null;
        return nodeEvent;
    }
    public void ExecuteNodeEvent()
    {
        NodeEvent nodeEvent = CreateNodeEvent();
        if (nodeEvent != null)
        {
            nodeEvent.ShowEventCanvas();
        }
    }
}
