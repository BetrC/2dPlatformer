using TheKiwiCoder;
using UnityEngine;
namespace BT
{
    public class IsHealthInRange : ActionNode
    {
        public HealthCheckType healthCheckType = HealthCheckType.Percent;

        [Tooltip("如果是值类型该参数为生命值，若为百分比类型则该参为生命值百分比")]
        public float minValue = 0;
        public float maxValue = 1;

        protected override void OnStart()
        {
            if (healthCheckType == HealthCheckType.Percent)
            {
                minValue = Mathf.Clamp01(minValue);
                maxValue = Mathf.Clamp01(maxValue);
            }
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            return InRange() ? State.Success : State.Failure;
        }

        private bool InRange()
        {
            float value = 0;

            if (healthCheckType == HealthCheckType.Percent)
                value = blackboard.CurHealth / blackboard.MaxHealth;
            else if (healthCheckType == HealthCheckType.Value)
                value = blackboard.CurHealth;

            return value >= minValue && value <= maxValue;
        }
    }
}
