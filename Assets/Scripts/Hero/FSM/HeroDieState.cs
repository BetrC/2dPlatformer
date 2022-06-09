
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class HeroDieState : HeroState
{

    public bool isAnimFinish = false;
    public ParticleSystem deathParticle;

    public HeroDieState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        isAnimFinish = false;
        SoundManager.Instance.PlaySound("hero_die");
    }

    IEnumerator Respawn()
    {
        PlayDeathParticle();
        hero.gameObject.SetActive(false);
        CameraManager.Instance.ShakeCamera(4, .2f);
        yield return new WaitForSeconds(.3f);
        hero.Respawn();
    }

    void PlayDeathParticle()
    {
        if (deathParticle == null && heroData.deathParticle != null)
            deathParticle = GameObject.Instantiate(heroData.deathParticle);
        if (deathParticle == null)
            return;
        
        deathParticle.transform.position = hero.transform.position;
        deathParticle.Play();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimFinish = true;
        DelegateCoroutine.Instance.StartCoroutine(Respawn());
    }
}