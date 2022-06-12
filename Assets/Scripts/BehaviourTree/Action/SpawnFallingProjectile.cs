
using TheKiwiCoder;
using UnityEngine;
using DG.Tweening;

public class SpawnFallingProjectile : ActionNode
{
    public BoxCollider2D spawnRangeBox;

    public AbstractProjectile prefab;

    public int spawnCount;

    public float spawnInterval;


    protected override void OnStart()
    {
        spawnRangeBox = GameObject.Find("SpawnRangeBox").GetComponent<BoxCollider2D>();
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Sequence s = DOTween.Sequence();
        for(int i = 0; i < spawnCount; i++)
        {
            s.AppendCallback(Spawn)
                .AppendInterval(spawnInterval);
        }
        return State.Success;
    }

    private void Spawn()
    {
        var randomX = Random.Range(spawnRangeBox.bounds.min.x, spawnRangeBox.bounds.max.x);
        AbstractProjectile projectile = Instantiate(prefab, new Vector3(randomX, spawnRangeBox.bounds.min.y), Quaternion.identity);
        projectile.Shooter = context.gameObject;
        projectile.SetForce(Vector2.zero);

    }


}
