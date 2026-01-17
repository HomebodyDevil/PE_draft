using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyCharacterViewSystem : Singleton<EnemyCharacterViewSystem>
{
    [SerializeField] private List<Transform> _enemyCharacterPositions = new();
    public List<CharacterView> EnemyCharacterViews { get; private set; }= new();

    private Coroutine _initialSettingCoroutine;
    
    protected override void Awake()
    {
        base.Awake();
        SetVars();
    }

    private void Start()
    {
        _initialSettingCoroutine = StartCoroutine(InitialSettingCoroutine());
    }

    private void OnEnable()
    {
        BattleEventSystem.Instance.OnCharacterDeath += DisableEnemyCharacterView;
    }

    private void OnDisable()
    {
        BattleEventSystem.Instance.OnCharacterDeath -= DisableEnemyCharacterView;
    }

    private void OnDestroy()
    {
        if (_initialSettingCoroutine != null)
        {
            StopCoroutine(_initialSettingCoroutine);
            _initialSettingCoroutine = null;
        }
    }

    private IEnumerator InitialSettingCoroutine()
    {
        yield return MakeEnemyCharacterViewsCoroutine();
        
        var battleEnemiesDataRef = PlayerStatusService.Instance.CurrentMapNodeStatus.BattleEnemiesData;

        var handle = battleEnemiesDataRef.LoadAssetAsync<BattleEnemiesData>();
        yield return handle;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Failed to load battle enemies data");
            yield break;
        }

        var battleEnemiesData = handle.Result;

        if (battleEnemiesData.Enemies.Count >
            EnemyCharacterViews.Count)
        {
            Debug.Log("more data than character view count");
        }
        
        for (int i = 0; i < battleEnemiesData.Enemies.Count; i++)
        {
            if (battleEnemiesData.Enemies[i] == null) continue;
            
            EnemyCharacterViews[i].SetCharacter(battleEnemiesData.Enemies[i]);
        }
        
        _initialSettingCoroutine = null;
        
        Addressables.Release(handle);
    }

    // View들을 생성 및 위치시킴.
    // _EnemyCharacterViews 리스트에 추가함.
    private IEnumerator MakeEnemyCharacterViewsCoroutine()
    {
        var locHandle = Addressables.LoadResourceLocationsAsync(
            new List<object>(){"Default", "CharacterView", "Enemy"},
            Addressables.MergeMode.Intersection,
            typeof(GameObject));
        yield return locHandle;

        if (locHandle.Status != AsyncOperationStatus.Succeeded || 
            locHandle.Result.Count == 0)
        {
            Debug.Log("Failed to load character view asset ref");
            yield break;
        }

        var characterViewAssetLoc = locHandle.Result[0];
        
        var assetHandle = Addressables.LoadAssetAsync<GameObject>(characterViewAssetLoc);
        yield return assetHandle;

        if (assetHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Failed to load character view asset");
            yield break;
        }
        
        var characterViewAsset = assetHandle.Result;

        for (int i = 0; i < _enemyCharacterPositions.Count; i++)
        {
            var characterViewInstance = 
                Instantiate(characterViewAsset, 
                    _enemyCharacterPositions[i],
                    false);
            
            EnemyCharacterViews.Add(characterViewInstance.GetComponent<CharacterView>());
        }
        
        Addressables.Release(locHandle);
        Addressables.Release(characterViewAsset);
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

    public CharacterView GetCharacterView(Character character)
    {
        foreach (var characterView in EnemyCharacterViews)
        {
            if (characterView.Character == character) return characterView;
        }

        return null;
    }
}
