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

    /// <summary>
    /// 获取某一动画的长度
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public static float GetAnimationClipLength(this Animator animator, string clipName)
    {
        
        AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(x => x.name == clipName);
        if (clip == null) 
            return 0;
        return clip.length;
    }

    /// <summary>
    /// 重置Animator的trigger参数状态
    /// </summary>
    /// <param name="animator"></param>
    public static void ResetAllTrigger(this Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger && animator.GetBool(param.name))
            {
                animator.ResetTrigger(param.name);
            }
        }
    }

}
