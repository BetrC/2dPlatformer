using UnityEngine;


public class ItemManager : Singleton<ItemManager>
{
    public void UseItem(Hero hero, ItemData item)
    {
        switch(item.useFunc)
        {
            case UseFunc.ActiveAbility:
                ActiveAbility((Ability)item.useParam);
                break;
            case UseFunc.HealHero:
                HealHero(hero, item.useParam);
                break;
            case UseFunc.GetStrawberry:
                GameManager.Instance.GetStrawberry(item.useParam);
                break;
        }
    }

    private void ActiveAbility(Ability ability)
    {
        AbilityManager.Instance.ActiveAbility(ability);
    }

    private void HealHero(Hero hero, int value)
    {
        hero.TakeDamage(-value);
    }
}

