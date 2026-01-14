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
        
        Debug.Log(MapNode.MapNodeStatus.StartTimeline == null ? "timeline is null" : "timeline is not null");
        Debug.Log(MapNode.MapNodeStatus.EnemyCharacterDatas[0].GetType().ToString());
    }
}
