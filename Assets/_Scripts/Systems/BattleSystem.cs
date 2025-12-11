using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleSystem : Singleton<BattleSystem>
{
    [SerializeField] private Camera _battleViewCamera;

    public Action<string> OnFindTarget;
    public Action<Character> OnFoundTarget;
    public Action<Character> OnLostTarget;
    public Action OnClearTargets;
    
    private List<Character> _currentTargets = new();

    protected override void Awake()
    {
        base.Awake();
        
        if (_battleViewCamera == null) transform.AssignChildVar<Camera>("BattleViewCamera", ref _battleViewCamera);
    }

    private void OnEnable()
    {
        OnFindTarget += FindTargetUnderMouse;
        OnFoundTarget += AddTarget;
        OnLostTarget += RemoveTarget;
        OnClearTargets += ClearTargets;
    }

    private void OnDisable()
    {
        OnFindTarget -= FindTargetUnderMouse;
        OnFoundTarget -= AddTarget;
        OnLostTarget -= RemoveTarget;
        OnClearTargets -= ClearTargets;
    }

    private void AddTarget(Character target)
    {
        if (target != null && !_currentTargets.Contains(target))
        {
            Debug.Log($"TragetAdded : {target.name}");
            _currentTargets.Add(target);
        }
    }

    private void RemoveTarget(Character target)
    {
        if (target != null) _currentTargets.Remove(target);
    }

    private void ClearTargets()
    {
        _currentTargets?.Clear();
    }

    private void FindTargetUnderMouse(string tag)
    {
        Ray ray = _battleViewCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        int layerMask = LayerMask.GetMask("Character", "Targetable");
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag(tag))
            {
                Debug.Log($"Gotcha. tag : {tag}");
                if (hit.transform.TryGetComponent(out Character character) && !_currentTargets.Contains(character))
                {
                    OnFoundTarget?.Invoke(character);
                }
            }
        }
    }
}
