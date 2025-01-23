using Actor.Unit.Component;
using UnityEngine;

public abstract class State<T> where T : Unit
{
    protected T Owner;
    protected StateMachine<T> StateMachine;
    protected int AnimBoolHash;
    protected int AnimationTriggerBit;

    public State(T owner, StateMachine<T> stateMachine, string animBoolName)
    {
        Owner = owner;
        StateMachine = stateMachine;
        AnimBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void Enter() { }
    public virtual void UpdateState() { }
    public virtual void Exit() { }
}