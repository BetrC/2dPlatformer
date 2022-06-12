using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnimationParamString;
using UnityEngine;


public class HeroLedgeClimbState : HeroState
{
    public bool isAnimFinish;

    public bool isHanging;

    public bool isClimbing;

    private bool wallDetected;
    private bool ledgeDetected;
    private bool onGround;

    private float xNormalInput;
    private float yNormalInput;

    private Vector2 startPos;
    private Vector2 endPos;

    public Vector2 cornerPos;

    public HeroLedgeClimbState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAnimFinish = false;
        isHanging = false;
        isClimbing = false;

        InitStartAndEndPos();

        hero.transform.position = startPos;
        hero.movement.SetVelocity(Vector2.zero);
        hero.movement.SetGravityScale(0f);
        hero.movement.SetBetterJumpEnable(false);
        hero.animator.SetBool(BOOL_LEDGE_CLIMB, false);
    }

    private void InitStartAndEndPos()
    {
        CalculateEdgeCornerPosition();

        startPos.Set(cornerPos.x - heroData.ledgeClimbStartPosOffset.x * FacingDirection,
                     cornerPos.y - heroData.ledgeClimbStartPosOffset.y);

        endPos.Set(cornerPos.x + heroData.ledgeClimbEndPosOffset.x * FacingDirection,
                   cornerPos.y + heroData.ledgeClimbEndPosOffset.y);

    }

    public override void Exit()
    {
        base.Exit();
        hero.movement.SetGravityScale(heroData.defaultGravityScale);
        hero.movement.SetBetterJumpEnable(true);
        hero.animator.SetBool(BOOL_LEDGE_CLIMB, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xNormalInput = InputManager.Instance.XNormalInput;
        yNormalInput = InputManager.Instance.YNormalInput;

        if (isAnimFinish)
        {
            hero.transform.position = endPos;
            stateMachine.ChangeState(hero.IdleState);
        }

        if (!isExitingState && !isClimbing && hero.WallJumpState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.WallJumpState);
        }
        else if (isHanging && !isClimbing && (xNormalInput == FacingDirection || yNormalInput == 1))
        {
            isClimbing = true;
            hero.animator.SetBool(BOOL_LEDGE_CLIMB, true);
        }

        if (isExitingState)
            return;

        hero.transform.position = startPos;

    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();

        wallDetected = hero.collisionChecker.WallDetected;
        ledgeDetected = hero.collisionChecker.LedgeDetected;
        onGround = hero.collisionChecker.OnGround;
    }

    private Vector2 CalculateEdgeCornerPosition()
    {
        float delta = 0.01f;
        RaycastHit2D xHit = Physics2D.Raycast(hero.collisionChecker.WallCheckPosition,
                                              hero.transform.right,
                                              hero.collisionChecker.wallCheckDistance,
                                              hero.collisionChecker.whatIsGround);
        float xDis = xHit.distance + delta;

        cornerPos.Set(xDis, 0);
        RaycastHit2D yHit = Physics2D.Raycast(hero.collisionChecker.LedgeCheckPosition + FacingDirection * cornerPos,
                                              Vector2.down,
                                              (hero.collisionChecker.LedgeCheckPosition - hero.collisionChecker.WallCheckPosition).y + delta,
                                              hero.collisionChecker.whatIsGround);
        cornerPos.Set(hero.collisionChecker.WallCheckPosition.x + xDis * FacingDirection,
            hero.collisionChecker.LedgeCheckPosition.y - yHit.distance);
        return cornerPos;
    }

    public bool IsTriggered()
    {
        return !hero.collisionChecker.OnGround && hero.collisionChecker.WallDetected && !hero.collisionChecker.LedgeDetected;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimFinish = true;
    }
}

