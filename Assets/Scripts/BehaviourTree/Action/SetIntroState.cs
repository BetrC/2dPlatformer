using TheKiwiCoder;

namespace BT
{
    public class SetIntroState : ActionNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }
        
        protected override State OnUpdate()
        {
            if (blackboard.Introed)
                return State.Failure;
            blackboard.Intro = true;
            return State.Success;
        }
    }
}