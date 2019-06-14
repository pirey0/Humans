using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using AI;
using UnityEditor;
using System.Linq;
using AI.Base;

namespace AI.Editor
{
    [CustomNodeEditor(typeof(DecisionNode))]
    public class DecisionNodeEditor : NodeEditor
    {
        public override Color GetTint()
        {
            return new Color(0.9f, 0.9f, 0.9f);
        }
    }
}