using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGraphGenerator : MonoBehaviour
{
    [SerializeField] AI.DecisionTreeGraph baseGraph;
    [SerializeField] string[] functions, actions;

    public AI.DecisionTreeGraph GetRandomGraph()
    {
        AI.DecisionTreeGraph newGraph = baseGraph.Copy() as AI.DecisionTreeGraph;


        return newGraph;
    }
}
