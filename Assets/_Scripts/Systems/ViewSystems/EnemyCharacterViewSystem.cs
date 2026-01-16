using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyCharacterViewSystem : Singleton<EnemyCharacterViewSystem>
{
    [SerializeField] private List<Transform> _enemyCharacterPositions = new();
    public List<CharacterView> EnemyCharacterViews { get; private set; }= new();
    
    protected override void Awake()
    {
        base.Awake();
        SetVars();
    }

    private void OnEnable()
    {
        BattleEventSystem.Instance.OnCharacterDeath += DisableEnemyCharacterView;
    }

    private void OnDisable()
    {
        BattleEventSystem.Instance.OnCharacterDeath -= DisableEnemyCharacterView;
    }
    
    private void EnableEnemyCharacterView(Character character)
    {
        foreach (var view in EnemyCharacterViews)
        {
            if (view.Character == character)
            {
                view.gameObject.SetActive(true);
            }
        }
    }

    private void DisableEnemyCharacterView(Character character)
    {
        foreach (var view in EnemyCharacterViews)
        {
            if (view.Character == character)
            {
                view.gameObject.SetActive(false);
            }
        }
    }
    
    private void SetCharacterPositions()
    {
        _enemyCharacterPositions.Clear();
        
        string enemyMatch = @"^EnemyCharacterPosition(100|[1-9]?\d)$";
        
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] == transform) continue;

            if (Regex.IsMatch(children[i].name, enemyMatch))
            {
                _enemyCharacterPositions.Add(children[i]);
            }
        }
    }

    // private void CreateEnemyCharacterView()
    // {
    //     Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Views/Character/DefaultEnemyView.prefab").Completed +=
    //         handle =>
    //         {
    //             GameObject enemyViewPrefab = handle.Result;
    //             for (int i = 0; i < EnemySystem.Instance.EnemyCharacters.Count; i++)
    //             {
    //                 GameObject go = Instantiate(enemyViewPrefab, _enemyCharacterPositions[i]);
    //                 if (go.TryGetComponent<CharacterView>(out CharacterView characterView))
    //                 {
    //                     characterView.SetCharacter(EnemySystem.Instance.EnemyCharacters[i]);
    //                     EnemyCharacterViews.Add(characterView);
    //                 }
    //             }
    //         };
    // }

    private void SetVars()
    {
        Transform tr = null;
        if (transform.AssignChildVar<Transform>("EnemyCharacterPositions", ref tr))
        {
            _enemyCharacterPositions.Clear();
            tr.GetComponentsInChildren<Transform>(true, _enemyCharacterPositions);
            _enemyCharacterPositions.Remove(tr);
        }

        SetCharacterPositions();
    }
}
