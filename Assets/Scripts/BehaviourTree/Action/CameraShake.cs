using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKiwiCoder;

public class CameraShake : ActionNode
{

    public float intensity = 3;
    public float time = 1f;

    protected override void OnStart()
    {
        CameraManager.Instance.ShakeCamera(intensity, time);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
