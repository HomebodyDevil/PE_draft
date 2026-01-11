using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerPortrait : MonoBehaviour
{
    [SerializeField] private Transform _speakerImage;
    
    public string SpeakerName { get; private set; }
    
    private void Awake()
    {
        if (_speakerImage == null) transform.AssignChildVar<Transform>("SpeakerImage", ref _speakerImage);
    }

    public void SetImage(Sprite sprite)
    {
        if (_speakerImage.TryGetComponent<Image>(out var image))
        {
            image.sprite = sprite;
        }
    }

    public void SetName(string name)
    {
        SpeakerName = name;
    }
}
