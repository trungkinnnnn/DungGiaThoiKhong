public class Transition
{
    public IStateBehaviour From;
    public IStateBehaviour To;
    public System.Func<bool> Condition;

    public Transition(IStateBehaviour from, IStateBehaviour to, System.Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}