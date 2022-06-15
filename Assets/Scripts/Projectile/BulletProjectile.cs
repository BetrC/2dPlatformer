using UnityEngine;
using System.Collections;

public class BulletProjectile : AbstractProjectile
{
    public float lifeTime = 4f;

    private Vector3 direction;

    public float speed;

    private void OnEnable()
    {
        StartCoroutine(RecycleThis(lifeTime));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator RecycleThis(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyProjectile();
    }

    

    public override void SetForce(Vector2 force)
    {
        this.force = force;
        direction = force.normalized;
    }

    protected override bool HitDestroySelf()
    {
        return true;
    }

    void Update()
    {
        transform.position += speed * direction * Time.deltaTime;
    }

    protected override void DestroyImp()
    {
        // 由对象池回收
        gameObject.SetActive(false);
    }
}
