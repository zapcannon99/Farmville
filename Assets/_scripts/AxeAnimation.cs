using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimation : MonoBehaviour
{
    public Animator animator;
    public bool isSwingingRight = false;
    public bool isSwingingLeft = false;
    public bool isHoldingLeft = false;
    public bool isHoldingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if tool is held
        if (Input.GetMouseButtonDown(0))
        {
            isHoldingLeft = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHoldingLeft = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            isHoldingRight = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHoldingRight = false;
        }

        // right swing
        if (isHoldingRight && Input.GetKeyDown(KeyCode.Q) && !isSwingingRight)
        {
            animator.SetBool("isSwingingRight", true);
            isSwingingRight = true;
        }
        else if (isHoldingRight && Input.GetKeyDown(KeyCode.Q) && isSwingingRight)
        {
            animator.SetBool("isSwingingRight", false);
            isSwingingRight = false;
        }

        // left swing
        if (isHoldingLeft && Input.GetKeyDown(KeyCode.Q) && !isSwingingLeft)
        {
            animator.SetBool("isSwingingLeft", true);
            isSwingingLeft = true;
        }
        else if (isHoldingLeft && Input.GetKeyDown(KeyCode.Q) && isSwingingLeft)
        {
            animator.SetBool("isSwingingLeft", false);
            isSwingingLeft = false;
        }
    }
}
