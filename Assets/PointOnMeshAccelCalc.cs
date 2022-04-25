using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PointOnMeshAccelCalc : MonoBehaviour
{
    public Transform cameraTransform;

    public Transform playerLeftConTransform;
    public Transform playerRightConTransform;
    public Transform parentTransform;
    public GameObject point;
    public GameObject leftHandController;
    public Transform minFlyingHeight;
    public Transform maxFlyingHeight;
    public float flyingSpeed = 0.5f;
    ILineRenderable m_LineRenderable;


    UnityEngine.XR.InputDevice device;
    UnityEngine.XR.InputDevice rightDevice;
    bool triggerPrsd = false;
    // Start is called before the first frame update
    void Start()
    {
        //get line renderer
        m_LineRenderable = leftHandController.GetComponent<ILineRenderable>();

        //get left hand devices
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            device = leftHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.role.ToString()));
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }

        //get right handed devices
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count == 1)
        {
            rightDevice = rightHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.role.ToString()));
        }
        else if (rightHandDevices.Count > 1)
        {
            Debug.Log("Found more than one right hand!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTrigger();
        flyUpFlyDown();
    }
    
    void flyUpFlyDown()
    {
        bool aPrsd;
        rightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out aPrsd);
        if(aPrsd)
        {
            Debug.Log("a prsd");
            if(minFlyingHeight.position.y <= transform.position.y)
            {
                Debug.Log("Before:" + transform.position);
                transform.position -= Vector3.up * flyingSpeed;
                Debug.Log("After:" + transform.position);
                //transform.position = new Vector3(transform.position.x, transform.position.y - flyingSpeed, transform.position.z);
            }
        }

        bool bPrsd;
        rightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bPrsd);
        if (bPrsd)
        {
            if (maxFlyingHeight.position.y >= transform.position.y)
            {
                Debug.Log("Before:" + transform.position);
                transform.position += Vector3.up * flyingSpeed;
                Debug.Log("After:" + transform.position);
                //cameraTransform.position = new Vector3(transform.position.x, transform.position.y + flyingSpeed, transform.position.z);
            }
        }

    }

    void CheckForTrigger()
    {
        bool triggerValue;
        if (device == null) return;
        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue);
        if (triggerValue)
        {
            if (!triggerPrsd)
            {
                triggerPrsd = true;
                lineRayCastMethod();
            }
        } 
        else
        {
            if (triggerPrsd)
            {
                triggerPrsd = false;
            }
        }
    }

    void lineRayCastMethod()
    {
        Vector3 m_ReticlePos;
        Vector3 m_ReticleNormal;
        int m_EndPositionInLine;

        if (m_LineRenderable.TryGetHitInfo(out m_ReticlePos, out m_ReticleNormal, out m_EndPositionInLine, out var isValidTarget))
        {
            //Debug.Log(m_ReticleNormal);
            //Debug.Log(m_EndPositionInLine);
            GameObject newPoint = Instantiate(point, m_ReticlePos, new Quaternion(0, 0, 0, 0));
            newPoint.transform.parent = parentTransform;
        }
    }

    void DoRaycastAndSpawnVectors()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(playerLeftConTransform.position, playerLeftConTransform.TransformDirection(Vector3.forward), out hit, 1000f))
        {
            Debug.DrawRay(playerLeftConTransform.position, playerLeftConTransform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            GameObject newPoint = Instantiate(point, playerLeftConTransform.TransformDirection(Vector3.forward) * hit.distance, new Quaternion(0,0,0,0));
            newPoint.transform.parent = parentTransform;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
}
