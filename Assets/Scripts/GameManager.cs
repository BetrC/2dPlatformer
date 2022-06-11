using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public float hitFreezeTime = .1f;

    public Transform respawnTransform;

    public Hero hero;

    protected override void Init()
    {
        base.Init();
        SceneManager.sceneLoaded += OnSceneLoaded;
        RecordRespawnPoint(FindObjectOfType<SceneInitPlayerPoint>().transform);
        hero = FindObjectOfType<Hero>();
    } 

    private void Start()
    {
        SoundManager.Instance.PlaySound("bgm");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        RecordRespawnPoint(FindObjectOfType<SceneInitPlayerPoint>().transform);
        hero.transform.position = respawnTransform.position;
        UIHealthManager.Instance.Reset();
    }

    public void HitFreezeTime()
    {
        FreezeTime(hitFreezeTime / 10);
    }
    public void FreezeTime(float duration)
    {
        Time.timeScale = 0.1f;
        StartCoroutine(UnfreezeTime(duration));
    }

    private IEnumerator UnfreezeTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1.0f;
    }

    public void RecordRespawnPoint(Transform transform)
    {
        respawnTransform = transform;
    }
}

