using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̳���MonoBehavior�ĵ���
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_instance;

    private static bool isInit = false;

    public static T Instance
    {
        get {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();
                if(m_instance == null)                
                    // ��������в����ڣ����ֶ�����һ��
                    m_instance = new GameObject("Mono_Sigleton" + typeof(T).Name, typeof(T)).GetComponent<T>();
            }

            if (!isInit)
            {
                isInit = true;
                m_instance.Init();
            }

            return m_instance;
        }
    }

    protected virtual void Init() { }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
        } else if (m_instance != this)
        {
            Debug.LogError("�����д��ڶ����ͬ���͵ĵ������� " + GetType().Name + "���ö��󽫻ᱻ����");
            DestroyImmediate(this);
            return;
        }

        if(!isInit)
        {
            DontDestroyOnLoad(gameObject);
            isInit = true;
            m_instance.Init();
        }
    }
}
