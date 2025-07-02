using Godot;
using System;

public abstract partial class Guard<T> : Node where T : Node
{
    public T _target { get; set; }
    public Transition<T> _transition;

    public void Init()
    {
        var parent = GetParent();
        if (parent is Transition<T>)
        {
            var transition = (Transition<T>)parent;
            _transition = transition;
            _target = transition._target;
            if (_target == null)
            {
                GD.Print("EGJEBJEBJE");
            }
        }
    }

    public abstract bool Check();
}