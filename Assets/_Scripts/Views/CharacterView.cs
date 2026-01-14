using System;
using PEEnum;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private CharacterData _defaultCharacterData;
    public Character Character { get; private set; }

    public Transform Text;
    
    private void Awake()
    {
        SetCharacter();
        
        if (Text == null) transform.AssignChildVar<Transform>("Panel", ref Text);
    }

    private void Start()
    {
        if (Text != null)
            Text.gameObject.SetActive(false);
        
        if (Character.TeamType.Team == Team.Enemy)
        {
            TestGA ga = new("Reacting to TurnEnd");
            
            Debug.Log("Test Reaction 등록");
            GameAbilitySystem.Instance.AddReaction<EndCharacterTurnGA>(
                ReactionTiming.Pre,
                Character,
                ga,
                ReactionTarget.Hostile,
                ConstValue.INFINITE_TURN_COUNT,
                false);
        }
    }

    public void SetCharacter(Character character=null)
    {
        if (character == null && _defaultCharacterData != null)
        {
            Character = new(_defaultCharacterData);
            return;
        }
        
        Character = character;
    }
}
