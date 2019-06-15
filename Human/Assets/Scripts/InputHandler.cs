using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{

    List<HumanController> hovering;
    HumanController[] selected;

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


        if (Input.GetMouseButtonDown(1))
        {

            if (hovering.Count > 0)
            {
                selected = hovering.ToArray();
            }
            else
            {
                selected = null;
            }

        }

    

    }

    public bool IsHovering(HumanController hc)
    {
        return hovering.Contains(hc);
    }

    public bool IsClickingOn(HumanController hc)
    {
        return Input.GetMouseButton(0) && IsHovering(hc);
    }


    private void OnGUI()
    {
        if(selected != null)
        {
            if(GUI.Button(new Rect(10, Screen.height - 100, 200,30), "Save " + selected.Length + " humans"))
            {
                GraphSaver.Instance.SaveHumans(selected, "Human");
            }

            if (GUI.Button(new Rect(10, Screen.height - 70, 200, 30), "Replace " + selected.Length + " humans"))
            {
                Spawner.Instance.Spawn(selected.Length);
                foreach (var s in selected)
                {
                    Destroy(s.gameObject);
                }
            }
        }
    }

}
