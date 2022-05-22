using UnityEngine;

// log 文本的类型
public enum ELogMsgType
{
    Normal,
    Warning,
    Error,
}

public static class CLog
{
    private const int defaultTextSize = 12;

    private static Color normalMsgColor = Color.cyan;
    private static Color warningMsgColor = Color.yellow;
    private static Color errorMsgColor = Color.red;


    // 颜色的文本格式
    public static string GetColorFormatStr(Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
    }

    // 设置文本颜色
    public static string SetTextColor(this string str, Color color)
    {
        string txt = string.Format(GetColorFormatStr(color)) + str + "</color>";
        return txt;
    }

    // 设置文本大小
    public static string SetTextSize(this string str, int size)
    {
        return $"<size={size}>{str}</size>";
    }

    // 设置文本为粗体
    public static string SetTextBold(this string str)
    {
        return $"<b>{str}</b>";
    }

    // 设置文本为斜体
    public static string SetTextItalic(this string str)
    {
        return $"<i>{str}</i>";
    }

    private static void LogMsg(ELogMsgType type, string msg)
    {
        msg = msg.SetTextSize(defaultTextSize);
        switch (type)
        {
            case ELogMsgType.Normal:
                Debug.Log(msg.SetTextColor(normalMsgColor));
                break;
            case ELogMsgType.Warning:
                Debug.Log(msg.SetTextColor(warningMsgColor));
                break;
            case ELogMsgType.Error:
                Debug.Log(msg.SetTextColor(errorMsgColor));
                break;
        }
    }

    public static void Log(object msg)
        => LogMsg(ELogMsgType.Normal, msg.ToString());



    public static void LogWarning(object msg)
        => LogMsg(ELogMsgType.Warning, msg.ToString());



    public static void LogError(object msg)
        => LogMsg(ELogMsgType.Error, msg.ToString());


}