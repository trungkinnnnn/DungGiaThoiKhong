using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public IStateBehaviour CurrentState { get; private set; }   
    private List<Transition> _transitions = new List<Transition>(); 
    private List<Transition> _anyTransitions = new List<Transition>();

    public void Init(IStateBehaviour startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
    private void CheckTransitions()
    {

        foreach (var transition in _anyTransitions)
        {
            if(transition.Condition())
            {
                ChangeState(transition.To);
                return;
            }
        }

        foreach (var transition in _transitions)
        {
            if (transition.From == CurrentState && transition.Condition())
            {
                ChangeState(transition.To);
                return ;
            }
        }

    }



    // =================== Service ====================

    public void ChangeState(IStateBehaviour newState)
    {
        if (CurrentState == newState) return;
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();

        //Debug.Log("Change State");
    }

    public void AddTransition(IStateBehaviour from, IStateBehaviour to, System.Func<bool> condition)
    {
        _transitions.Add(new Transition(from, to, condition));
    }

    public void AddAnyTransition(IStateBehaviour to, System.Func<bool> condition)
    {
        _anyTransitions.Add(new Transition(null, to, condition));
    }

    public void Update()
    {
        CurrentState?.Update();
        CheckTransitions();
    }

    
}
