using TheKiwiCoder;
using Unity.Mathematics;
using UnityEngine;

namespace BT
{
    public class PlayParticle : ActionNode
    {
        public ParticleSystem particle;
        
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            ParticleSystem p = GameObject.Instantiate(particle, context.transform.position, quaternion.identity);
            p.Play();
            return State.Success;
        }
    }
}