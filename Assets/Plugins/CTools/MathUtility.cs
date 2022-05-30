using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class MathUtility
{
    /// <summary>
    /// 标准化一个float
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int Normalize(this float value)
    {
        if (value == 0) return 0;
        return (int)Mathf.Sign(value);
    }

    /// <summary>
    /// 对两个vector3按分量相乘
    /// </summary>
    /// <param name="one"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Vector3 Multiple(this Vector3 one, Vector3 other)
    {
        one.x *= other.x;
        one.y *= other.y;
        one.z *= other.z;
        return one;
    }
}
