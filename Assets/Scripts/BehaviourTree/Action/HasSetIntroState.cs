using TheKiwiCoder;

namespace BT
{
    public class HasSetIntroState : ActionNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            return blackboard.Intro ? State.Success : State.Failure;
        }
    }
}