using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject guard;
    private Vector3 prevPos;
    private Vector3 prevRot;
    private bool mirror = false; //if true, guard will match instead of mirror
    private bool trackRot = true;
    private bool trackPos = true;
    private Vector3 playerOrigin = new Vector3(3.8f, 1f, 2.3f);
    private Vector3 guardOrigin = new Vector3(-0.2f, 1f, 2.3f);

    // Start is called before the first frame update
    void Start()
    {
        prevPos = this.transform.position;
        prevRot = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //move player back to origin
        {
            this.GetComponent<OVRPlayerController>().enabled = false;
            this.GetComponent<CharacterController>().enabled = false;

            this.transform.position = playerOrigin;

            this.GetComponent<OVRPlayerController>().enabled = true;
            this.GetComponent<CharacterController>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.F)) //rotate player 180 degrees
        {
            this.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.M)) //switch between mirroring/matching
        {
            mirror = !mirror;

            //move cube to match new mode
            Vector3 playerDistFromOrigin = this.transform.position - playerOrigin;
            Vector3 playerRotation = this.transform.eulerAngles;
            Vector3 guardDistFromOrigin = guard.transform.position - guardOrigin;

            if (mirror)
            {
                guard.transform.Translate(-guardDistFromOrigin - playerDistFromOrigin, Space.World);
                guard.transform.Rotate(-guard.transform.eulerAngles - playerRotation, Space.World);
            }
            else //match
            {
                guard.transform.Translate(playerDistFromOrigin - guardDistFromOrigin, Space.World);
                guard.transform.Rotate(playerRotation - guard.transform.eulerAngles, Space.World);
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) //toggle rotation tracking - I decided to make this "O" since R moves the camera
        {
            trackRot = !trackRot;
        }

        if (Input.GetKeyDown(KeyCode.P)) //toggle position tracking
        {
            trackPos = !trackPos;
        }

        //update guard position
        Vector3 positionChange = this.transform.position - prevPos;
        Vector3 rotChange = this.transform.eulerAngles - prevRot;

        if (mirror) //guard mirroring
        {
            if (trackPos)
            {
                guard.transform.Translate(-positionChange, Space.World);
            }
            if (trackRot)
            {
                guard.transform.Rotate(-rotChange, Space.World);
            }
        }
        else //guard matching
        {
            if (trackPos)
            {
                guard.transform.Translate(positionChange, Space.World);
            }
            if (trackRot)
            {
                guard.transform.Rotate(rotChange, Space.World);    
            }
        }

        prevPos = this.transform.position;
        prevRot = this.transform.eulerAngles;
        
    }
}
