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
    public static readonly string FLOAT_HORIZONTAL_INPUT = "horizontalInput";
    public static readonly string FLOAT_VERTICAL_INPUT = "verticalInput";
    // X方向速度
    public static readonly string FLOAT_SPEEDX = "speedX";

    // Y方向速度
    public static readonly string FLOAT_SPEEDY = "speedY";

    // 地面检测，是否在地面上
    public static readonly string BOOL_GROUND = "onGround";

    public static readonly string BOOL_WALL_SLIDE = "wallSlide";

    public static readonly string BOOL_WALL_GRAB = "wallGrab";

    public static readonly string BOOL_IS_CASTING_SKILL = "isCastingSkill";

    public static readonly string TRIGGER_HIT = "hit";

    public static readonly string TRIGGER_DIE = "die";

    public static readonly string BOOL_IDLE = "idle";

    public static readonly string BOOL_RUN = "run";

    public static readonly string BOOL_JUMP = "jump";

    public static readonly string BOOL_DASH = "dash";

    public static readonly string BOOL_ATTACK = "attack";

    public static readonly string BOOL_INAIR = "inAir";

    public static readonly string INT_ATTACK_SEQUENCE = "attackSeq";




}
