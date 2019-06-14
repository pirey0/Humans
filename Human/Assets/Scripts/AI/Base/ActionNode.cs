using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace AI.Base
{
    public class ActionNode : DecisionNode
    {
        public System.Action Action;

        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}