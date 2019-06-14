using AI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace AI
{
    public class ReflectiveActionNode : ActionNode
    {
        [SerializeField] string actionName;

        public override object GetValue(NodePort port)
        {
            if (brain != null)
            {
                if(actionName == "" || actionName == "null" || actionName == "Nothing")
                {
                    Action = null;
                }
                else
                {
                    UnityEngine.Profiling.Profiler.BeginSample("GetReflectiveAction");
                    Action = brain.GetAction(actionName);
                    UnityEngine.Profiling.Profiler.EndSample();
                }
            }
            else
            {
                Debug.LogWarning("No brain");
            }

            return base.GetValue(port);
        }
    }
}
