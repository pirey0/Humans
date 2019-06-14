using AI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace AI
{

    public class StartNode : Node
    {
        [SerializeField] [Output] private DecisionNode output;
        [SerializeField] [Input] private DecisionNode rootNode;

        public override object GetValue(NodePort port)
        {
            if(port.fieldName == "output")
            {
                DecisionNode n = GetInputValue<DecisionNode>("rootNode", this.output);
                return n;
            }

            throw new System.Exception("requesting nonexistent port");
        }
    }

}