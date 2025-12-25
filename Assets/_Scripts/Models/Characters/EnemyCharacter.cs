using System.Collections;
using UnityEngine;

public class EnemyCharacter : Character
{
    private Coroutine _enemyTurnCoroutine;
    
    public EnemyCharacter(CharacterData data) : base(data)
    {
        
    }
    
    public override void StartTurn()
    {
        Debug.Log("EnemyStartTurn");
        if (_enemyTurnCoroutine != null)
        {
            CoroutineRunnerService.Instance.StopCoroutine(_enemyTurnCoroutine);
            _enemyTurnCoroutine = null;
        }
        
        _enemyTurnCoroutine = CoroutineRunnerService.Instance.StartCoroutine(EnemyTurnCoroutine());
    }

    private IEnumerator EnemyTurnCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        
        Debug.Log("Done");
    
        EndCharacterTurnGA endTurnGA = new(this);
        GameAbilitySystem.Instance.RequestPerformGameAbility(this, new() { endTurnGA });
        
        _enemyTurnCoroutine = null;
    }
}
