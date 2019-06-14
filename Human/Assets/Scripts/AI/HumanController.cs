using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] AI.DecisionTreeGraph graph;

    [Space(10)]
    [SerializeField] float sadSpeed;
    [SerializeField] float normalSpeed, happySpeed;

    AI.DecisionTreeBrain treeBrain;
    NavMeshAgent agent;
    Animator animator;

    private bool inAnimation = false;
    private float mood;
    private float animationTimeTracker;

    private void Start()
    {
        treeBrain = new AI.DecisionTreeBrain(this, graph);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        RandomizeMood();
    }


    private void Update()
    {
        var action = treeBrain.Think();

        if(action != null)
        {
            action.Invoke();
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animationTimeTracker -= Time.deltaTime;

        if (HasPath())
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    #region functions

    public bool InAnimation()
    {
        return animationTimeTracker > 0;
    }

    public bool HasPath()
    {
        return agent.hasPath;
    }

    public bool RandomChoice()
    {
        return Random.value > 0.5f;
    }

    #endregion


    #region actions

    public void RandomizeMood()
    {
        SetMood((Mood)Random.Range(0, 3));
    }

    public void RandomizeDestination()
    {
        agent.destination = new Vector3(Random.value * 100 -50, 0, Random.value * 100 -50);
    }


    #endregion


    private void SetMood(Mood m)
    {
        switch (m)
        {
            case Mood.Sad:
                agent.speed = sadSpeed;
                mood = 0;
                UpdateAnimatorMood();
                break;

            case Mood.Neutral:
                agent.speed = normalSpeed;
                mood = 0.5f;
                UpdateAnimatorMood();
                break;
            case Mood.Happy:
                agent.speed = happySpeed;
                mood = 1;
                UpdateAnimatorMood();
                break;
        }
    }

    private void UpdateAnimatorMood()
    {
        animator.SetFloat("Mood", mood);
    }

    private enum Mood
    {
        Sad,
        Neutral,
        Happy
    }
}
