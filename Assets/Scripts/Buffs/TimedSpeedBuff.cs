
using ScriptableObjects;
using UnityEngine;

public class TimedSpeedBuff : TimedBuff
{
    private readonly Enemie _movementComponent;

    public TimedSpeedBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        //Getting MovementComponent, replace with your own implementation
        _movementComponent = obj.GetComponent<Enemie>();
    }

    protected override void ApplyEffect()
    {
        //Add speed increase to MovementComponent
        ScriptableSpeedBuff speedBuff = (ScriptableSpeedBuff)Buff;
        _movementComponent.SetSpeed(speedBuff.SpeedIncrease);
    }

    public override void End()
    {
        //Revert speed increase
        ScriptableSpeedBuff speedBuff = (ScriptableSpeedBuff)Buff;
        _movementComponent.SetSpeed(speedBuff.SpeedIncrease);
        EffectStacks = 0;
    }
}