using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class TweenUtility
{
    public static void DoMovePingPong(this Transform transform, Vector3 left, Vector3 right, float speed = 1f, float yAngle = 0f)
    {
        float duration = (transform.position - right).magnitude / speed;
        transform.rotation = Quaternion.Euler(new Vector3(
            transform.eulerAngles.x, yAngle, transform.eulerAngles.z));
        transform.DOMove(right, duration)
                 .OnComplete(() => DoMovePingPong(transform, right, left, speed, 180 - yAngle));
    }
}
