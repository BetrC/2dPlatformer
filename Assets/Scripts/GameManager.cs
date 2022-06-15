using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnStrawBerryNumUpdate : UnityEvent<int> { };

public class OnHeroCreated : UnityEvent { };

public class GameManager : MonoSingleton<GameManager>
{
    public float hitFreezeTime = .1f;

    public Transform respawnTransform;

    public Hero heroPrefab;
    public Hero hero;

    public int strawberryCount = 0;

    public const int maxStrawberryCount = 12;

    internal int waitToLoadSceneIndex = -1;

    public OnStrawBerryNumUpdate StrawBerryNumUpdate = new();
    public OnHeroCreated OnHeroCreated = new();

    protected override void Init()
    {
        base.Init();
        SceneManager.sceneLoaded += OnSceneLoaded;

        hero = FindObjectOfType<Hero>();
    }

    public void AddStrawberry(int useParam)
    {
        strawberryCount++;
        StrawBerryNumUpdate.Invoke(strawberryCount);
    }

    public bool IsCurSceneBoosScene()
    {
        return SceneManager.GetActiveScene().name == "Level-Boss";
    }

    public bool IsCurSceneLevelScene()
    {
        return IsLevelScene(SceneManager.GetActiveScene().name);
    }

    private bool IsLevelScene(string name)
    {
        return name.StartsWith("Level");
    }

    private void FindOrCreateHero()
    {
        hero = FindObjectOfType<Hero>();
        if (hero == null)
        {
            hero = GameObject.Instantiate(heroPrefab);
            OnHeroCreated.Invoke();
        }
        hero.transform.position = respawnTransform.position;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (IsLevelScene(scene.name))
        {
            var respawnPoint = FindObjectOfType<SceneInitPlayerPoint>();
            RecordRespawnPoint(respawnPoint.transform);
            FindOrCreateHero();
            UIHealthManager.Instance.Reset();
        }
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

    public void SetTimeScale(float scale)
    {
        StopAllCoroutines();
        Time.timeScale = scale;
    }

    public void RecordRespawnPoint(Transform transform)
    {
        respawnTransform = transform;
    }

    public void StartGame()
    {
        LoadLevel(2);
    }

    public void RestartGame()
    {
        ResetData();
        StartGame();
    }

    public void ReturnToMenu()
    {
        ResetData();
        SceneManager.LoadScene("start");
    }

    public void LoadLevel(int levelIndex)
    {
        waitToLoadSceneIndex = levelIndex;
        SceneManager.LoadSceneAsync("transition");
    }


    public void ResetData()
    {
        strawberryCount = 0;
        hero.health.Reset();
        AbilityManager.Instance.ResetAllAbility();
    }
}

