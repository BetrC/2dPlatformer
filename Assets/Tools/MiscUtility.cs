using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 杂项
public static class MiscUtility
{
    // 当前Unix时间戳，根据当前系统时间计算
    public static long GetNowUnixTimeSeconds()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }


}
