
using UnityEngine;

public class Boss : Enemy
{

    protected override void Start()
    {
        base.Start();
        BindHealthBar();
    }


    private void BindHealthBar()
    {
        if (healthBar == null)
            Bind(GameObject.Find("BossHealthBar").GetComponent<HealthBar>(), false);
        else
            Bind(healthBar, false);
    }
}