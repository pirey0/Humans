using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using XNode;

namespace AI.Base
{
    public class ActionNode : DecisionNode
    {
        public MethodInfo Action;

        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}