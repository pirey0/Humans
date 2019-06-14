using AI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class HasBrainDecisionNode : DecisionNode
    {
        protected override bool IsTrue()
        {
            return brain != null;
        }
    }
}