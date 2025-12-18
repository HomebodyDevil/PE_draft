using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : PersistantSingleton<SceneService>
{
    [SerializeField] private float _minWaitTime = 1.5f;
    
    private Coroutine _sceneLoadCoroutine;
    private List<string> _loadingScenes = new();
    
    public void ChangeScene(string scene)
    {
        _sceneLoadCoroutine = StartCoroutine(LoadSceneCoroutine(scene));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        if (_loadingScenes.Contains(sceneName))
            yield break;
        
        _loadingScenes.Add(sceneName);
        float time = 0f;
        
        var loadSceneOP = SceneManager.LoadSceneAsync(sceneName);
        loadSceneOP.allowSceneActivation = false;

        while (!loadSceneOP.isDone)
        {
            time += Time.deltaTime;
            Debug.Log($"load Scene Progress :  {loadSceneOP.progress * 100}%");

            if (time >= _minWaitTime)
            {
                loadSceneOP.allowSceneActivation = true;
            }

            yield return null;
        }

        _loadingScenes.Remove(sceneName);
    }
}
