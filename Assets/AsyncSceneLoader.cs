using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{

    public float atLeastShowTime = 2f;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.unscaledTime;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(GameManager.Instance.waitToLoadSceneIndex);
        async.allowSceneActivation = false;

        while(async.progress < 0.9f || (Time.unscaledTime < startTime + atLeastShowTime))
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }

}
