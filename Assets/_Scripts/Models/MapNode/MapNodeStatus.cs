using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Timeline;

[Serializable]
public class MapNodeStatus
{
    [field: SerializeField] public AssetReferenceT<TimelineAsset> StartTimeline { get; set; } = null;
    [field: SerializeField] public AssetReferenceT<BattleEnemiesData> BattleEnemiesData { get; set; } = null;
    
    public MapNodeStatus() { }

    public MapNodeStatus(MapNodeData mapNodeData)
    {
        StartTimeline = mapNodeData.StartTimeline;
        BattleEnemiesData = mapNodeData.BattleEnemiesData;
    }
}
