using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{

    public float atLeastShowTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(GameManager.Instance.waitToLoadSceneIndex);
        async.allowSceneActivation = false;

        while(async.progress < 0.9f || (Time.time < Time.timeSinceLevelLoad + atLeastShowTime))
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }

}
