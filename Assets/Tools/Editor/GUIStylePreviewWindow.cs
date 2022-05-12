using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

public class GUIStylePreviewWindow : EditorWindow
{
    static List<GUIStyle> styles;

    [MenuItem("Window/GUITools/GUIStylePreview")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<GUIStylePreviewWindow>("GUI Style Preview");

        styles = new List<GUIStyle>();
        foreach(PropertyInfo info in typeof(EditorStyles).GetProperties(
            BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
        {
            object o = info.GetValue(null, null);
            if(o.GetType() == typeof(GUIStyle))
            {
                styles.Add(o as GUIStyle);
            }

        }
    }

    public Vector2 scrollPosition = Vector2.zero;
    private void OnGUI()
    {
        if (styles == null)
            return;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for(int i = 0; i < styles.Count; i++)
        {
            GUILayout.Label($"EditorStyles.{styles[i].name}", styles[i]);
        }
        GUILayout.EndScrollView();
    }
}
