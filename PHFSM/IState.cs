public interface IState
{
	void Init(); // for init before start (PH)FSM
	void Enter(); // for transition
	void Exit(); // for changing
	void PhysicsUpdate(double delta);
	void Process(double delta);
}
