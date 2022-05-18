using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class FPSDisplay : MonoBehaviour
{
    public float showTime = 1f;
    public Text text;

    private int m_count = 0;
    private float m_deltaTime = 0f;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        m_count++;
        m_deltaTime += Time.unscaledDeltaTime;
        if (m_deltaTime >= showTime)
        {
            float fps = m_count / m_deltaTime;
            text.text = $"FPS: {Mathf.RoundToInt(fps)}";
            m_count = 0;
            m_deltaTime = 0f;
        }
    }
}
