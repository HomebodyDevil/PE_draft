using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Timeline;

public class MapNodeStatus
{
    public AssetReferenceT<TimelineAsset> StartTimeline { get; set; } = null;
    public List<AssetReferenceT<CharacterData>> EnemyCharacterDatas { get; set; } = new();
    
    public MapNodeStatus() { }

    public MapNodeStatus(MapNodeData mapNodeData)
    {
        StartTimeline = mapNodeData.StartTimeline;
        EnemyCharacterDatas.AddRange(mapNodeData.EnemyCharacterDatas);
    }
}
