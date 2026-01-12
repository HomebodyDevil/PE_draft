using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SpeakerPortrait : MonoBehaviour
{
    [SerializeField] private Transform _speakerImage;

    public AsyncOperationHandle<GameObject> GameObjectOperationHandle;
    public AsyncOperationHandle<Sprite> SpriteOperationHandle;
    
    public string SpeakerName { get; private set; }
    
    private void Awake()
    {
        if (_speakerImage == null) transform.AssignChildVar<Transform>("SpeakerImage", ref _speakerImage);
    }

    private void Start()
    {
        if (_speakerImage.TryGetComponent<Image>(out var image))
        {
            image.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (SpriteOperationHandle.IsValid())
        {
            Addressables.Release(SpriteOperationHandle);
        }

        if (GameObjectOperationHandle.IsValid())
        {
            Addressables.Release(GameObjectOperationHandle);
        }
    }
    
    public void AddressableSetup(
        AsyncOperationHandle<GameObject> goHandle,
        AsyncOperationHandle<Sprite> spriteHandle)
    {
        GameObjectOperationHandle = goHandle;
        SpriteOperationHandle = spriteHandle;
    }

    public void SetImage(Sprite sprite)
    {
        if (_speakerImage.TryGetComponent<Image>(out var image))
        {
            image.sprite = sprite;
        }
    }

    public void SetVisible(bool visible)
    {
        if (_speakerImage.TryGetComponent<Image>(out var image))
        {
            image.enabled = visible;
        }
    }

    public void SetName(string name)
    {
        SpeakerName = name;
    }
}
