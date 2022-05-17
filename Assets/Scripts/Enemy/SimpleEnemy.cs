using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SimpleEnemy : Actor2D
{
    public Vector3 moveLeftPoint;
    public Vector3 moveRightPoint;

    public float moveSpeed = 5f;

    private Vector3 leftPoint;
    private Vector3 rightPoint;

    protected override void Awake()
    {
        leftPoint = transform.position + moveLeftPoint;
        rightPoint = transform.position + moveRightPoint;
    }

    private void OnEnable()
    {
        StartCoroutine(MoveAround());
    }

    private void OnDisable()
    {
        StopCoroutine(MoveAround());
    }

    public IEnumerator MoveAround()
    {
        while(true)
        {
            Vector3 dir = transform.position - leftPoint;
            float yAngle = 0;
            if (dir.x < 0)
                yAngle = 180;
            transform.rotation = Quaternion.Euler(0, yAngle, 0);

            Vector2 target = dir.x < 0 ? leftPoint : rightPoint;
            Vector2 newPos = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            transform.position = newPos;
            yield return null;
        }
    }
}
