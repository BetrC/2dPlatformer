using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "newFightNumber", menuName = "Data/FightNumber")]
public class FightNumberConfig : ScriptableObject
{
    [Header("移动")]
    public Vector2 initPos;
    public Vector2 targetPos;
    public float moveTime = .3f;
    public AnimationCurve moveCurve;

    [Header("缩放")]
    public Vector2 initScale;
    public Vector2 targetScale;
    public float scaleTime = .3f;
    public AnimationCurve scaleCurve;

    [Header("颜色")]
    public Color initColor = Color.white;
    public Color targetColor = Color.white;
    public float colorTime = 1f;
    public AnimationCurve colorCurve;
}
