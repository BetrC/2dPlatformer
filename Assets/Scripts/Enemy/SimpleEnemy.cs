using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(Health))]
public class SimpleEnemy : DamageableActor
{
    public Vector3 moveLeftPoint;
    public Vector3 moveRightPoint;

    public float moveSpeed = 2f;

    private Vector3 leftPoint;
    private Vector3 rightPoint;


    // hit
    [Header("hit player")]
    public LayerMask whatIsPlayer;
    public float hitCD = .1f;
    public Vector2 hitDir = Vector2.one;
    public float hitForce = 2f;

    private float lastHitTime;

    protected override void Awake()
    {
        base.Awake();
        leftPoint = transform.position + moveLeftPoint;
        rightPoint = transform.position + moveRightPoint;
    }

    private void OnEnable()
    {
        transform.DoMovePingPong(leftPoint, rightPoint, moveSpeed, 0f);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    void HitCheck()
    {
        
    }
}
