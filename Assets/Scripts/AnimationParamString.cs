using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 枚举角色的 animator parameter
/// </summary>
public static class AnimationParamString
{
    public static string FLOAT_HORIZONTAL_INPUT = "horizontalInput";
    public static string FLOAT_VERTICAL_INPUT = "verticalInput";
    // X方向速度
    public static string FLOAT_SPEEDX = "speedX";

    // Y方向速度
    public static string FLOAT_SPEEDY = "speedY";

    // 是否在移动
    public static string BOOL_MOVING = "moving";

    // 地面检测，是否在地面上
    public static string BOOL_GROUND = "onGround";

    public static string BOOL_WALL_SLIDE = "wallSlide";

    public static string BOOL_WALL_GRAB = "wallGrab";
}
