using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModifierController : MonoBehaviour
{
    public GameObject AvatarObjects;

    public static float CrawlingMovementModifier = 0.1f;
    public static float CrouchingMovementModifier = 0.33f;
    public static float RunningMovementModifier = 2f;

    public static float StandingHeight = 1.5f;
    public static float CrouchedHeight = .75f;
    public static float CrawlingHeight = .4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPosition = AvatarObjects.transform.localPosition;

        if(Input.GetButton("Crawl") && !Input.GetButton("Crouch") && !Input.GetButton("Run")) {
            AvatarObjects.transform.localPosition = new Vector3(currentPosition.x, CrawlingHeight, currentPosition.z);
        } else if(Input.GetButton("Crouch") && !Input.GetButton("Run")) {
            AvatarObjects.transform.localPosition = new Vector3(currentPosition.x, CrouchedHeight, currentPosition.z);
        } else {
            AvatarObjects.transform.localPosition = new Vector3(currentPosition.x, StandingHeight, currentPosition.z);
        }
    }
}
