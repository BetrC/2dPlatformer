using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float hitFreezeTime = .2f;

    private void Start()
    {
        SoundManager.Instance.PlaySound("bgm");
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
}

