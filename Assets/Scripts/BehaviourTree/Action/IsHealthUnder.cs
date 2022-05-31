using System.Collections;
using UnityEngine;
using TheKiwiCoder;

namespace BT
{

    public enum HealthCheckType
    {
        Value,
        Percent,
    }

    public class IsHealthUnder : ActionNode
    {
        public HealthCheckType healthCheckType = HealthCheckType.Percent;

        [Tooltip("如果是值类型该参数为生命值，若为百分比类型则该参为生命值百分比")]
        public float value;


        protected override void OnStart()
        {
            if (healthCheckType == HealthCheckType.Percent)
            {
                value = Mathf.Clamp01(value);
            }
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            if (blackboard.MaxHealth <= 0)
                return State.Success;
            if (healthCheckType == HealthCheckType.Value)
                return blackboard.CurHealth <= value ? State.Success : State.Failure;
            else
                return (blackboard.CurHealth / blackboard.MaxHealth) <= value ? State.Success : State.Failure;
        }
    }
}