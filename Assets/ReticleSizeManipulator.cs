using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReticleSizeManipulator : MonoBehaviour
{

    public GameObject leftReticlePrefab;
    public GameObject rightRetclePrefab;

    public Transform leftController;
    public Transform rightController;

    public float reticleSizeMultiplier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnMeshHoverLeft(HoverEnterEventArgs args)
    {
        RaycastHit rayInfo;
        if(Physics.Raycast(leftController.position, leftController.rotation.eulerAngles, out rayInfo, 30f))
        {
            //leftReticlePrefab.transform.localScale = (Vector3.one) * 0.05f * rayInfo.distance * reticleSizeMultiplier;
        } 
    }

    public void OffMeshHoverLeft()
    {
        //leftReticlePrefab.transform.localScale = (Vector3.one) * 0.05f;
    }

    public void OnMeshHoverRight(HoverEnterEventArgs args)
    {
        RaycastHit rayInfo;
        if (Physics.Raycast(rightController.position, rightController.rotation.eulerAngles, out rayInfo, 30f))
        {
            //rightRetclePrefab.transform.localScale = (Vector3.one) * 0.05f * rayInfo.distance * reticleSizeMultiplier;
        }
    }

    public void OffMeshHoverRight()
    {
        //rightRetclePrefab.transform.localScale = (Vector3.one) * 0.05f;
    }
}
