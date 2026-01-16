using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerCharacterViewSystem : Singleton<PlayerCharacterViewSystem>
{
    [SerializeField] private List<Transform> _playerCharacterPositions = new(); 
    
    protected override void Awake()
    {
        base.Awake();
        SetVars();
    }

    private void Start()
    {
        SetPlayerCharacters();
    }

    private void SetPlayerCharacters()
    {
        Debug.Log("Setting PlayerCharacters");
    }

    // private void CreateCharacterView()
    // {
    //     Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Views/Character/DefaultPlayerView.prefab").Completed +=
    //         handle =>
    //         {
    //             GameObject playerViewPrefab = handle.Result;
    //             for (int i = 0; i < PlayerStatusService.Instance.PlayerStatus.PlayerCharacters.Count; i++)
    //             {
    //                 Debug.Log("여기선 0으로만 했는데, 나중에 i를 사용하도록 바꾸자");
    //                 Instantiate(playerViewPrefab, _playerCharacterPositions[0]);
    //             }
    //         };
    // }

    private void SetVars()
    {
        Transform tr = null;
        if (transform.AssignChildVar<Transform>("PlayerCharacterPositions", ref tr))
        {
            _playerCharacterPositions.Clear();
            tr.GetComponentsInChildren<Transform>(true, _playerCharacterPositions);
            _playerCharacterPositions.Remove(tr);
        }

        SetCharacterPositions();
    }

    private void SetCharacterPositions()
    {
        _playerCharacterPositions.Clear();

        string playerMatch = @"^PlayerCharacterPosition(100|[1-9]?\d)$";
        
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] == transform) continue;

            if (Regex.IsMatch(children[i].name, playerMatch))
            {
                _playerCharacterPositions.Add(children[i]);
            }
        }
    }
}
