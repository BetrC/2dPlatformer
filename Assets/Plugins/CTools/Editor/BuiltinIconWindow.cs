using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BuiltinIconWindow : EditorWindow
{
    [MenuItem("Window/GUITools/ShowBuiltinIcons")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<BuiltinIconWindow>("Unity Builtin Icons");
    }

    private Vector2 m_Scroll;
    private List<string> m_Icons;

    private void Awake()
    {
        m_Icons = new List<string>();

        Texture2D[] textures = Resources.FindObjectsOfTypeAll<Texture2D>();
        foreach(Texture2D texture in textures) {
            Debug.unityLogger.logEnabled = false;
            GUIContent gc = EditorGUIUtility.IconContent(texture.name);
            Debug.unityLogger.logEnabled = true;
            if(gc != null && gc.image != null)
            {
                m_Icons.Add(texture.name);
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Click To Copy Texture Name", new GUIStyle("ToolTip"));
        m_Scroll = GUILayout.BeginScrollView(m_Scroll);
        float width = 50f;
        int count = (int)(position.width / width);
        for(int i = 0; i < m_Icons.Count; i+= count)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < count; j++) {
                int index = i + j;
                if(index < m_Icons.Count)
                {
                    if (GUILayout.Button(EditorGUIUtility.IconContent(m_Icons[index]), GUILayout.Width(width), GUILayout.Height(30)))
                    {
                        CLog.Log($"Texture Name : {m_Icons[index]} copyed to clipboard");
                        UnityEngine.GUIUtility.systemCopyBuffer = m_Icons[index];
                    }
                }
            }
            GUILayout.EndHorizontal();

        }
        EditorGUILayout.EndScrollView();
    }
}
