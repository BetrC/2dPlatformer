//using UnityEngine;
//using System.Collections.Generic;

//public class WeaponManager : MonoSingleton<WeaponManager>
//{
//    public List<Weapon> playerWeapons;

//    public int CurWeaponIndex { get; set; }

//    public Weapon GetWeapon()
//    {
//        return playerWeapons[CurWeaponIndex];
//    }

//    public Weapon SetWeapon(int index)
//    {
//        CurWeaponIndex = index;
//    }

//    public void AddWeapon(Weapon weapon)
//    {
//        playerWeapons.Add(weapon);
//    }

//    public void RemoveWeapon(Weapon weapon)
//    {
//        playerWeapons.Remove(weapon);
//    }
//}
