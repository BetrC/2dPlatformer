using TheKiwiCoder;
using UnityEngine;
using DG.Tweening;

public class SpawnWaveProjectile : ActionNode
{

    public AbstractProjectile prefab;

    public int spawnCount;

    public float spawnInterval;

    public Transform spawnTrans;

    protected override void OnStart()
    {
        spawnTrans = context.transform.Find("SpawnWave");
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Sequence s = DOTween.Sequence();
        for (int i = 0; i < spawnCount; i++)
        {
            s.AppendCallback(Spawn)
                .AppendInterval(spawnInterval);
        }
        return State.Success;
    }

    public void Spawn()
    {
        AbstractProjectile projectile = Instantiate(prefab, spawnTrans.position, Quaternion.identity);
        projectile.Shooter = context.gameObject;
        projectile.transform.rotation = spawnTrans.rotation;
        projectile.SetForce(Vector2.right * context.movement.FacingDirection);
    }
}

