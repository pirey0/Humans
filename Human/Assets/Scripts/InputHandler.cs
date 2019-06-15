using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{

    List<HumanController> hovering;

    void Start()
    {
        hovering = new List<HumanController>();   
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        hovering.Clear();
        foreach (var hit in hits)
        {
            var hc = hit.transform.GetComponent<HumanController>();
            if(hc != null)
            {
                hovering.Add(hc);
            }
        }

    }

    public bool IsHovering(HumanController hc)
    {
        return hovering.Contains(hc);
    }

    public bool IsClickingOn(HumanController hc)
    {
        return Input.GetMouseButtonDown(0) && IsHovering(hc);
    }
}
