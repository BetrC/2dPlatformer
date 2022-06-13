using UnityEngine;


public enum UseFunc
{
    ActiveAbility,
    HealHero,
    GetStrawberry,
}


[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public string Name;

    public Sprite icon;

    public UseFunc useFunc;

    public int useParam;

    public string sound = "hero_get_ability";

    public ParticleSystem particle;
}
