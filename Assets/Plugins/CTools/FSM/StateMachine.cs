
public class StateMachine
{
    public State currentState { get; private set; }

    public void Init(State initState)
    {
        currentState = initState;
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
