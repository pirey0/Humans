using AI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGraphGenerator : Singleton<RandomGraphGenerator>
{
    [SerializeField] AI.DecisionTreeGraph baseGraph;
    [SerializeField] string[] functions, actions;

    public AI.DecisionTreeGraph GetRandomGraph()
    {
        AI.DecisionTreeGraph newGraph = baseGraph.Copy() as AI.DecisionTreeGraph;

        List<string> fs = new List<string>(functions);

        var arr = newGraph.nodes.ToArray();

        foreach (var node in arr)
        {
            if (node is AI.ReflectiveDecisionNode)
            {
                CompleteNode(newGraph, node as AI.ReflectiveDecisionNode, ref fs, -1200);
            }
        }
        return newGraph;
    }


    private void CompleteNode(AI.DecisionTreeGraph graph, AI.ReflectiveDecisionNode node, ref List<string> fs, int x)
    {
        var p1 = node.GetPort("trueNode");
        var p2 = node.GetPort("falseNode");

        if (p1.ConnectionCount == 0)
        {
            if(fs.Count > 0 && Random.value > 0.5f)
            {
                var newNode = graph.AddNode<AI.ReflectiveDecisionNode>();
                var pout = newNode.GetPort("output");
                pout.Connect(p1);
                int index = Random.Range(0, fs.Count);
                newNode.MethodName = fs[index];
                fs.RemoveAt(index);
                newNode.position = new Vector2(x, 0);

                CompleteNode(graph, newNode, ref fs, x -300);
            }
            else
            {
               var newNode = graph.AddNode<AI.ReflectiveActionNode>();
                var pout = newNode.GetPort("output");
                pout.Connect(p1);
                newNode.ActionName = actions[Random.Range(0, actions.Length)];
                newNode.position = new Vector2(x, 0);
            }
            
        }

        if(p2.ConnectionCount == 0)
        {
            if (fs.Count > 0 && Random.value > 0.5f)
            {
                var newNode = graph.AddNode<AI.ReflectiveDecisionNode>();
                var pout = newNode.GetPort("output");
                pout.Connect(p2);
                int index = Random.Range(0, fs.Count);
                newNode.MethodName = fs[index];
                fs.RemoveAt(index);
                newNode.position = new Vector2(x, 0);

                CompleteNode(graph, newNode, ref fs, x - 300);
            }
            else
            {
                var newNode = graph.AddNode<AI.ReflectiveActionNode>();
                var pout = newNode.GetPort("output");
                pout.Connect(p2);
                newNode.ActionName = actions[Random.Range(0, actions.Length)];
                newNode.position = new Vector2(x, 0);
            }
        }

    }



    private void OnGUI()
    {
        if(GUI.Button(new Rect(Screen.width-200, 10, 200, 30), "Save new Random Tree"))
        {
            GraphSaver.Instance.SaveGraph(GetRandomGraph(), "SavedTree");
        }
    }

}
