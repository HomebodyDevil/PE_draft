using System;
using UnityEngine;

public class SpeakerPortrait : MonoBehaviour
{
    [SerializeField] private Transform _speakerImage;
    
    private void Awake()
    {
        if (_speakerImage == null) transform.AssignChildVar<Transform>("SpeakerImage", ref _speakerImage);
    }
}
