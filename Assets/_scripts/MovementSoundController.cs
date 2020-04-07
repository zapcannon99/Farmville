using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSoundController : MonoBehaviour
{
    public GameObject WalkingSound;
    public GameObject CrouchedSound;
    public GameObject CrawlingSound;
    public GameObject RunningSound;

    public enum MovementStates {
        idle,
        walking,
        running,
        crouching,
        crawling
    }

    private MovementStates _previousState;
    private GameObject _previousSound;

    // Start is called before the first frame update
    void Start()
    {
        _previousState = MovementStates.idle;
    }

    // Update is called once per frame
    void Update()
    {
        // FSM
        MovementStates currentState = MovementStates.idle;

        // check if not moving right now
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            // check if running (running has highest priority in multi button input
            if(Input.GetButton("Run")) {
                currentState = MovementStates.running;
            } else if(Input.GetButton("Crouch")) {
                currentState = MovementStates.crouching;
            } else if(Input.GetButton("Crawl")) {
                currentState = MovementStates.crawling;
            } else {
                currentState = MovementStates.walking;
            }
        }

        // if the state has changed, we need to change sounds
        if(currentState != _previousState) {
            // Deactivate previous sound if any
            if(_previousSound != null) {
                _previousSound.SetActive(false);
            }

            //Crawling has the same sound as crouched, but should be louder
            switch(currentState) {
                case MovementStates.crawling:
                    CrawlingSound.SetActive(true);
                    _previousSound = CrawlingSound;
                    break;
                case MovementStates.crouching:
                    CrouchedSound.SetActive(true);
                    _previousSound = CrouchedSound;
                    break;
                case MovementStates.running:
                    RunningSound.SetActive(true);
                    _previousSound = RunningSound;
                    break;
                case MovementStates.walking:
                    WalkingSound.SetActive(true);
                    _previousSound = WalkingSound;
                    break;
                default:
                    break;

            }
        }

        // Save current current state to be referenced for next update
        _previousState = currentState;

        // TODO: When we get a chance later, need to do a raycase down to check if 
        // we are over water, and if so, disable all ground sounds for something else
    }
}
