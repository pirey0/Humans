using AI.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace AI.Editor
{
    [CustomNodeEditor(typeof(ActionNode))]
    public class ActionNodeEditor : NodeEditor
    {
        public override Color GetTint()
        {
            return new Color(0.4f,1,0.4f);
        }

        public override void OnBodyGUI()
        {
            serializedObject.Update();
            string[] excludes = { "m_Script", "graph", "position", "ports", "trueNode", "falseNode" };
            portPositions = new Dictionary<XNode.NodePort, Vector2>();

            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;
            EditorGUIUtility.labelWidth = 84;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (excludes.Contains(iterator.name)) continue;
                NodeEditorGUILayout.PropertyField(iterator, true);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    
}
