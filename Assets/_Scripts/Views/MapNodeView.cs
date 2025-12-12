using UnityEngine;

public class MapNodeView : MonoBehaviour
{
    public MapNode MapNode { get; private set; }

    public MapNodeView(MapNode mapNode)
    {
        MapNode = mapNode;
    }
    
    public void OnClick()
    {
        SceneService.Instance.ChangeScene("BattleScene");
    }
}
