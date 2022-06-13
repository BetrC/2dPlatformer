

using System.Collections.Generic;

public enum Ability
{
    SecondJump = 1,
    SecondAttack = 2,
    Dash = 3,
    Shoot = 4,
}

public class AbilityManager : MonoSingleton<AbilityManager>
{
    Dictionary<Ability, bool> activedAbilitys = new();


    public bool IsAbilityActive(Ability ability)
    {
        if (activedAbilitys.TryGetValue(ability, out bool active))
            return active;
        return false;
    }

    public void ActiveAbility(Ability ability)
    {
        activedAbilitys[ability] = true;
    }
}
