using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Reflection;
using System;
using System.Linq.Expressions;
using AI.Base;

namespace AI
{

    [CreateAssetMenu]
    public class DecisionTreeGraph : NodeGraph
    {
        private DecisionTreeBrain brain = null;


        public DecisionTreeBrain Brain
        {
            get
            {
                return brain;
            }
        }

  

        public void SetBrain( DecisionTreeBrain b)
        {

            brain = b;
        }

        public void SetupBrainReferencesInNodes()
        {
            foreach (var n in nodes)
            {
                var dn = n as DecisionNode;
                if (dn != null)
                {
                    dn.SetBrain(brain);
                }
            }
        }
    }


    public class DecisionTreeBrain
    {
        private DecisionTreeGraph graph;
        private StartNode rootNode;
        private MonoBehaviour controller;
        private Dictionary<string, MethodInfo> gottenFunctions; //lists to avoid getting functions already calculated
        private Dictionary<string, MethodInfo> gottenMethods;


        public MonoBehaviour Controller
        {
            get
            {
                return controller;
            }
        }
        public DecisionTreeGraph Graph
        {
            get
            {
                return graph;
            }
        }

        public DecisionTreeBrain( MonoBehaviour controller, DecisionTreeGraph originGraph)
        {
            this.controller = controller;
            graph = (DecisionTreeGraph)originGraph.Copy();
            graph.SetBrain(this);
            graph.SetupBrainReferencesInNodes();
            rootNode = GetRootNode();

            gottenMethods = new Dictionary<string, MethodInfo>();
            gottenFunctions = new Dictionary<string, MethodInfo>();
        }

        private StartNode GetRootNode()
        {
            foreach (var node in graph.nodes)
            {
                if (node is StartNode)
                {
                    return node as StartNode;
                }
            }

            throw new System.Exception("No Root node found");
        }

        public virtual MethodInfo Think()
        {
            UnityEngine.Profiling.Profiler.BeginSample("DecisionTree.Think()");

            DecisionNode node = rootNode.GetValue(rootNode.GetOutputPort("output")) as DecisionNode;
            UnityEngine.Profiling.Profiler.EndSample();
            if (node is ActionNode)
            {
                return ((ActionNode)node).Action;
            }
            else
            {
                throw new System.Exception("Decision tree doesn't end in an action: " + ((node == null) ? "Null" : node.ToString()));
            }

        }
        
        public MethodInfo GetFunction(string name)
        {
            if (gottenFunctions.ContainsKey(name))
            {
                return gottenFunctions[name];
            }


            var method = controller.GetType().GetMethod(name,
              System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (method == null || !method.ReturnType.Equals(typeof(bool)))
            {
                throw new Exception("Could not find requested method " + name);
            }

            gottenFunctions.Add(name, method);
            return method;

        }

        public MethodInfo GetAction(string name)
        {
            if (gottenMethods.ContainsKey(name))
            {
                return gottenMethods[name];
            }

            var method = controller.GetType().GetMethod(name,
              System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (method == null || !method.ReturnType.Equals(typeof(void)))
            {
                throw new Exception("Could not find requested method " + name);
            }

            gottenMethods.Add(name, method);
            return method;
        }

    }
}