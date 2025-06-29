using Godot;
using System;

public abstract partial class Guard<T> : Node where T : Node
{
    protected T _target { get; private set; }
    protected Transition<T> _transition;

    public void Init()
    {
        var parent = GetParent();
        if (parent is Transition<T>)
        {
            var transition = (Transition<T>)parent;
            _transition = transition;
            _target = transition._target;
        }
    }

    public abstract bool Check();
}