using UnityEngine;

public class MapNodeView : MonoBehaviour
{
    public MapNode MapNode { get; private set; }

    public MapNodeView(MapNode mapNode)
    {
        MapNode = mapNode;
    }

    public void SetMapNode(MapNode mapNode)
    {
        MapNode = mapNode;
    }
    
    public void OnClick()
    {
        SceneService.Instance.ChangeScene("BattleScene");
        if (MapNode == null)
        {
            Debug.Log("MapNodeView: MapNode is null");
            return;
        }

        Debug.Log($"MapNode Type: {MapNode.NodeType.ToString()}");
        Debug.Log("PlayerStatusService의 MapNodeStatus Set.");
        PlayerStatusService.Instance.CurrentMapNodeStatus = MapNode.MapNodeStatus;
    }
}
