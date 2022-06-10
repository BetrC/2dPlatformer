using TheKiwiCoder;

namespace BT
{
    public class DestroyEnemy : ActionNode
    {
        protected override void OnStart()
        {
            if (context.gameObject != null)
            {
                Destroy(context.gameObject);
            }
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}