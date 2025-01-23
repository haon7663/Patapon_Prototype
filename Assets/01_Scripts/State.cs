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

    public virtual void Enter()
    {
        Owner.Animator.SetBool(AnimBoolHash, true);
    }
    public virtual void UpdateState() { }

    public virtual void Exit()
    {
        Owner.Animator.SetBool(AnimBoolHash, false);
    }

    public void AnimationTrigger(AnimationTriggerEnum bit)
    {
        AnimationTriggerBit |= (int)bit;
    }
    
    public bool IsTriggerCalled(AnimationTriggerEnum bit)
        => (AnimationTriggerBit & (int)bit) != 0;
    public void RemoveTrigger(AnimationTriggerEnum bit)
        => AnimationTriggerBit &= ~(int)bit;
}