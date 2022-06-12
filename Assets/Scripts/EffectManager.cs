using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager>
{
    public void PlayOneShot(ParticleSystem particleSystem, Vector3 position)
    {
        if (particleSystem == null) return;

        var effect = Instantiate(particleSystem, position, Quaternion.identity);
        effect.Play();

        var duration = effect.main.duration + effect.main.startLifetime.constantMax;
        effect.gameObject.AddComponent<Disposable>().lifeTime = duration;
    }
}
