using TheKiwiCoder;

public class ExcuteOnce : DecoratorNode
{
    public State successType = State.Success;

    public bool hasExcuted = false;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (hasExcuted)
            return State.Failure;
        var state = child.Update();

        if (state == successType)
            return State.Success;
        else
            return State.Failure;

    }
}
