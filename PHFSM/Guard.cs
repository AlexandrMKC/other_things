using Godot;
using System;

public abstract partial class Guard<T> : Node where T: Node
{
	protected T _target;
	protected Transition<T> _transition;

	public void Init(){
		var transitionNode = GetParent();
		if(transitionNode is Transition<T>){
			var transition = (Transition<T>)transitionNode;
			_transition = transition;
			_target = transition._stateMachine._target;
			if(_target == null){
				GD.Print("Error");
			}
		}
	}

	public abstract bool Check();
}
