using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoughInputHandler : MonoBehaviour
{
    Animator animator;
    [SerializeField] ParticleSystem ps;
    [SerializeField] int emitAmount;
    [SerializeField] float coughChancePerSec;
    private void Start()
    {
       animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Cough");
        }


        if(Random.value < coughChancePerSec * Time.deltaTime)
        {
            animator.SetTrigger("Cough");
        }
    }

    private void Cough()
    {
        ps.Emit(emitAmount);
    }
    
}
