using TheKiwiCoder;

namespace BT
{
    public class EnemyIsDead : ActionNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            return blackboard.Dead ? State.Success : State.Failure;
        }
    }
}