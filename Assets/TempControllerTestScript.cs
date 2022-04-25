using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TempControllerTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemSelected()
    {
        Debug.Log("item selected?");
    }

    public void OnBallItemSelcted()
    {
        Debug.Log("ball was selected");
    }

    public void onHoverObj(HoverEnterEventArgs args)
    {
        Transform hoveredOnTransform = args.interactableObject.transform;
        //Debug.Log($"x:{transform.position.x} y:{transform.position.y} z:{transform.position.z}");
    }
}
