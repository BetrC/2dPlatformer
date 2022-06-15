
using UnityEngine;

public class Boss : Enemy
{

    protected override void Start()
    {
        base.Start();
        FaceHero();
    }

    private void FaceHero()
    {
        float xDiff = GameManager.Instance.hero.transform.position.x - transform.position.x;
        xDiff = xDiff >= 0 ? 1 : -1;
        movement.CheckFlip((int)xDiff);
    }


    protected override void BindHealthBar()
    {
        if (healthBar == null)
            Bind(GameObject.Find("BossHealthBar").GetComponent<HealthBar>());
        else
            Bind(healthBar);
    }
}