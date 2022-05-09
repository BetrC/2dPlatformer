using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class MathUtility
{
    public static float Normalize(this float value)
    {
        if (value == 0) return 0;
        return Mathf.Sign(value);
    }
}
