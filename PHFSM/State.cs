using Godot;
using System;
using System.Collections;

[GlobalClass]
public partial class State : Node, IState
{
    [Export]
    public string name;

    [Export]
    public Node PHFSM;

    [Export]
    public Node context;
    protected ArrayList _transitions = new ArrayList();

    public void Init()
    {
        _transitions.Clear();
        
        foreach (var child in GetChildren())
        {
            if (child is Transition)
            {
                var transition = (Transition)child;
                transition.Init();
                _transitions.Add(transition);
                GD.Print("State: add transition " + transition.Name);
            }
        }
    }

    public void CheckTransitions()
    {
        foreach (var transition in _transitions)
        {
            var transition_ = (Transition)transition;
            transition_.CheckGuards();
        }
    }

    public void test(double delta)
    {
        CheckTransitions();
        PhysicsUpdate(delta);
    }

    public virtual void Enter() { }

    public virtual void Exit(){ }

    public virtual void PhysicsUpdate(double delta){ }

    public virtual void Process(double delta){ }
}
