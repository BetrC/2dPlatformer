using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class HeroDashState : HeroState
{
    public DashTrail dashTrail;

    public HeroDashState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (dashTrail == null)
            dashTrail = GameObject.FindObjectOfType<DashTrail>();
        dashTrail.ShowTrail(hero);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
