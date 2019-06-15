using AI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace AI
{
    public class ReflectiveActionNode : ActionNode
    {
        [SerializeField] public string ActionName;

        public override object GetValue(NodePort port)
        {
            if (brain != null)
            {
                if(ActionName == "" || ActionName == "null" || ActionName == "Nothing")
                {
                    Action = null;
                }
                else
                {
                    UnityEngine.Profiling.Profiler.BeginSample("GetReflectiveAction");
                    Action = brain.GetAction(ActionName);
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
