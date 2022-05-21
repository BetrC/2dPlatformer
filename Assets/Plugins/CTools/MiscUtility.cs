using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 杂项
public static class MiscUtility
{
    // 当前Unix时间戳s，根据当前系统时间计算
    public static long GetNowUnixTimeSeconds()
    {
        return new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
    }

    // 当前Unix时间戳ms，根据当前系统时间计算
    public static long GetNowUnixTimeMilliSeconds()
    {
        return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
        
    }
}
