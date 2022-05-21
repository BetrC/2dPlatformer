using UnityEngine;

[CreateAssetMenu(fileName = "newHeroData", menuName = "Data/Hero Data")]
public class HeroData : ScriptableObject
{
    [Header("Walk")]
    public float runSpeed = 5f;

    [Header("Jump")]
    public float jumpSpeed = 25f;

    /// <summary>
    /// 跳跃的土狼时间，使玩家在离开地面后一段时间仍可以起跳
    /// </summary>
    public float coyoteTime = .15f;

    /// <summary>
    /// 落下的输入检测时间，若玩家在即将落地前键入跳跃，在落地后会自动起跳
    /// </summary>
    public float jumpFallThreshould = .1f;

    public int canJumpTime = 1;

    [Header("Dash")]
    public float dashSpeed = 40f;

    public float dashTime = .4f;

    [Header("Particle")]
    public ParticleSystem runParticle;
    public ParticleSystem jumpParticle;

}