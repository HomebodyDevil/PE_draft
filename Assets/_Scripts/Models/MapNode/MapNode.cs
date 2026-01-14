using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum NodeType
{
    None,
    Rest,
    Battle,
    Elite,
}

public class MapNode
{
    public NodeType NodeType { get; private set; }
    public List<MapNode> ParentNode { get; private set; } = new();
    public List<MapNode> ChildNode { get; private set; } = new();
    public int NodeLevel { get; private set; } = 0;
    public MapNodeStatus MapNodeStatus { get; private set; } = null;
    
    public MapNode(int nodeLevel, NodeType nodeType = NodeType.None)
    {
        NodeLevel = nodeLevel;
        NodeType = nodeType;
    }

    public void SetMapNodeData(MapNodeData mapNodeData)
    {
        MapNodeStatus = new(mapNodeData);
    }
    
    // 보류
    // public void AddNode(bool isChildNode, MapNode node)
    // {
    //     if (node == null)
    //     {
    //         Debug.Log("Node is null");
    //         return;
    //     }
    //     
    //     var list = isChildNode ? ChildNode : ParentNode;
    //     list.Add(node);
    // }
    //
    // public void RemoveNode(bool isChildNode, MapNode node)
    // {
    //     if (node == null)
    //     {
    //         Debug.Log("Node is null");
    //         return;
    //     }
    //     
    //     var list = isChildNode ? ChildNode : ParentNode;
    //     list.Remove(node);
    // }
}
