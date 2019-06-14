using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace AI.Base
{

    ////////////////////////
    /// Base node class for the decision tree system
    ///////////////////////
    public class DecisionNode : Node
    {
        [SerializeField] [Output] private DecisionNode output;

        [SerializeField] [Input] private DecisionNode trueNode;
        [SerializeField] [Input] private DecisionNode falseNode;

        protected DecisionTreeBrain brain;
        
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "output")
            {
                if (IsTrue())
                {
                    return GetInputValue<DecisionNode>("trueNode", trueNode);
                }
                else
                {
                    return GetInputValue<DecisionNode>("falseNode", falseNode);
                }
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        protected virtual bool IsTrue()
        {
            throw new System.Exception("Calling unimplemented function DecisionNode.IsTrue()");
        }

        public void SetBrain( DecisionTreeBrain b)
        {
            brain = b;
        }
    }
}