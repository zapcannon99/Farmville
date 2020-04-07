using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Malimbe.XmlDocumentationAttribute;
using Malimbe.PropertySerializationAttribute;

public class ManipulationController : MonoBehaviour
{
    public enum Side { left, right }

    public Side ControllerSide = Side.left;

    public GameObject OtherController;

    public GameObject CollidingObject;

    Quaternion previousAngle;
    Vector3 previousPosition;

    public bool isHolding;

    // Start is called before the first frame update
    void Start()
    {
        previousAngle = transform.rotation;
        previousPosition = transform.position;
        isHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        // poll and do actions
        if (CollidingObject)
        {
            Quaternion angularSpeed = Quaternion.Inverse(previousAngle) * transform.rotation;
            Vector3 linearSpeed = transform.position - previousPosition;
            Rigidbody rb = CollidingObject.GetComponent<Rigidbody>();
            ManipulationController otherController = OtherController.GetComponent<ManipulationController>();
            //First check if the other side is grabing
            if (Input.GetMouseButton((int)ControllerSide))
            {
                bool isSameObject = false;
                if(otherController.isHolding)
                {
                    isSameObject = otherController.CollidingObject.Equals(CollidingObject);
                }
                if(!isHolding && !isSameObject)
                {
                    rb.isKinematic = true;
                    rb.transform.SetParent(gameObject.transform);
                    UnHighlightObject();
                }
                isHolding = true;
            }
            else if (Input.GetMouseButtonUp((int)ControllerSide))
            {
                isHolding = false;
                bool isSameObject = false;
                if (otherController.isHolding)
                {
                    isSameObject = otherController.CollidingObject.Equals(CollidingObject);
                }
                if (!isSameObject)
                {
                    rb.transform.SetParent(GameObject.Find("/World/Environment/Interactables").transform);
                    rb.isKinematic = false;
                    rb.velocity = linearSpeed / Time.deltaTime / rb.mass;
                    rb.angularVelocity = angularSpeed.eulerAngles / Time.deltaTime / rb.mass;
                    HighlightObject();
                }
                else
                {
                    rb.transform.SetParent(otherController.transform);
                } 
            }
        }

        //Spaghetti Sanity check for isHolding
        if (isHolding)
        {
            if (!CollidingObject)
                isHolding = false;
        }

        previousPosition = transform.position;
        previousAngle = transform.rotation;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ineffcient sadly, but there may be another way to avoid checking this on every trigger enter
        if (!isHolding && other.gameObject.layer != LayerMask.NameToLayer("ControllerLayer") && other.gameObject.GetComponent<Rigidbody>() != null)
        {
            CollidingObject = other.gameObject;
            HighlightObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isHolding)
        {
            UnHighlightObject();
            CollidingObject = null;
        }
    }

    private void HighlightObject()
    {
        var rend = CollidingObject.GetComponent<MeshRenderer>();
        Material[] mats = rend.materials;
        if(mats.Length < 2)
        {
            Material[] temp = { mats[0], Globals.GetHighlightMaterial() };
            rend.materials = temp;
        }
    }

    private void UnHighlightObject()
    {
        var rend = CollidingObject.GetComponent<MeshRenderer>();
        Material[] mats = rend.materials;
        if (mats.Length > 1)
        {
            Material[] temp = { mats[0] };
            rend.materials = temp;
        }
    }
}

//function OnMouseOver()
//{
//    renderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
//}

//function OnMouseExit()
//{
//    renderer.material.shader = Shader.Find("Diffuse");
//}