using System;
using UnityEngine;

[Serializable]
public class RuntimeWeaponInfo
{
    public GameObject prefab;

    [HideInInspector]
    public GameObject runtimeObj;

    [HideInInspector]
    public Weapon weapon;

    private Transform parent;

    public void Init(Transform parent)
    {
        this.parent = parent;
        SetWeapon(prefab);
    }

    public void SetWeapon(GameObject prefab)
    {
        if (this.prefab != prefab)
            this.prefab = prefab;
        GameObject go = GameObject.Instantiate(this.prefab, parent);
        if (runtimeObj != null)
        {
            GameObject.Destroy(runtimeObj);
        }
        runtimeObj = go;
        weapon = runtimeObj.GetComponent<Weapon>();
    }
}
