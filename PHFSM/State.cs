using Godot;
using System;
using System.Collections.Generic;

public abstract partial class State<T>: Node where T: Node
{
    [Export]
    public string name;

    public StateMachine<T> _stateMachine { get; set; }

    protected T _target;

    protected List<Transition<T>> _transitions = new List<Transition<T>>();

    public void Init()
    {
         _stateMachine = (StateMachine<T>)GetParent();
        if(_stateMachine == null){
            GD.Print("State Machine error: _stateMachine == null");
        }

        _target = _stateMachine._target;
        if(_target == null){
            GD.Print("State Machine error: _target == null");
        }

        _transitions.Clear(); 
        foreach (var child in GetChildren())
        {
            if (child is Transition<T>)
            {
                var transition = (Transition<T>)child;
                transition.Init();
                transition.TransitionTo += _stateMachine.ChangeState;
                _transitions.Add(transition);

                GD.Print("State Machine" + this.Name + " state " + this.Name + ": add transition " + transition.Name);
            }
        }
    }

    public void CheckTransitions()
    {
        foreach (var transition in _transitions)
        {
            transition.Check();
        }
    }

    public void PhysicsUpdate(double delta)
    {
        InternalPhysicalProcesses(delta);
        GD.Print("STATE Internal " + this.Name);
        CheckTransitions();
        GD.Print("STATE CheckTransition " + this.Name);
    }

    public void Update(double delta)
    {
        InternalProcesses(delta);
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void InternalProcesses(double delta);

    public abstract void InternalPhysicalProcesses(double delta);
}
