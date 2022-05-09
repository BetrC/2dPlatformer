using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承自MonoBehavior的单例
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
                    // 如果场景中不存在，则手动创建一个
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
            Debug.LogError("场景中存在多个相同类型的单例对象： " + GetType().Name + "，该对象将会被销毁");
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
