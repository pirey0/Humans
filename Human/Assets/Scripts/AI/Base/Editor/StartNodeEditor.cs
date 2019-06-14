using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;

namespace AI.Editor
{
    [CustomNodeEditor(typeof(StartNode))]
    public class StartNodeEditor : NodeEditor
    {
        public override Color GetTint()
        {
            return new Color(1, 0.4f, 1);
        }
    }
}

