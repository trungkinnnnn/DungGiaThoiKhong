public interface IStateBehaviour
{
    void Enter();
    void Update();
    void Exit();    
}

public interface IComBat
{
    void SetParaCombat(bool value);
}