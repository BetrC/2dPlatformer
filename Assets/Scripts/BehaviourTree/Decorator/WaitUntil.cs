using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKiwiCoder;

namespace BT
{
    public class WaitUntil : DecoratorNode
    {
        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            var state = child.Update();
            if (state == State.Success)
                return State.Success;
            else
                return State.Running;
        }
    }
}
