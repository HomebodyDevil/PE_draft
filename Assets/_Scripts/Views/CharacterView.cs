using System;
using UnityEngine;

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
        Text.gameObject.SetActive(false);
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
