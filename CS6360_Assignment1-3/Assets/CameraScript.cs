using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject guard;
    private Vector3 prevPos;
    private bool mirror = false; //if true, guard will match instead of mirror
    private bool trackRot = true;
    private bool trackPos = true;
    private Vector3 playerOrigin = new Vector3(3.8f, 1f, 2.3f);
    private Vector3 guardOrigin = new Vector3(-0.2f, 1f, 2.3f);
    private Camera headsetCamera;

    // Start is called before the first frame update
    void Start()
    {
        headsetCamera = GameObject.Find("CenterEyeAnchor").GetComponentInChildren<Camera>();
        prevPos = headsetCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = headsetCamera.transform.position;
        Vector3 playerRot = headsetCamera.transform.eulerAngles;

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
            resetGuardPosition(playerPos, playerRot);
        }

        if (Input.GetKeyDown(KeyCode.O)) //toggle rotation tracking - I decided to make this "O" since R moves the camera
        {
            trackRot = !trackRot;
        }

        if (Input.GetKeyDown(KeyCode.P)) //toggle position tracking
        {
            trackPos = !trackPos;
            if (trackPos)
            {
                resetGuardPosition(playerPos, playerRot);
            }
        }

        //update guard position
        Vector3 positionChange = playerPos - prevPos;

        if (mirror) //guard mirroring
        {
            if (trackPos)
            {
                guard.transform.Translate(new Vector3(-positionChange[0], positionChange[1], positionChange[2]), Space.World);
            }
            if (trackRot)
            {
                guard.transform.rotation = Quaternion.Euler(new Vector3(playerRot[0], -playerRot[1], -playerRot[2]));
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
                guard.transform.rotation = Quaternion.Euler(playerRot);
            }
        }

        prevPos = playerPos;
        
    }

    void resetGuardPosition(Vector3 playerPos, Vector3 playerRot)
    {
        Vector3 playerDistFromOrigin = playerPos - playerOrigin;
        Vector3 guardDistFromOrigin = guard.transform.position - guardOrigin;

        if (mirror)
        {
            guard.transform.Translate(new Vector3(-guardDistFromOrigin[0] - playerDistFromOrigin[0], -guardDistFromOrigin[1] + playerDistFromOrigin[1], -guardDistFromOrigin[2] + playerDistFromOrigin[2]), Space.World);
            guard.transform.rotation = Quaternion.Euler(-playerRot);
        }
        else //match
        {
            guard.transform.Translate(playerDistFromOrigin - guardDistFromOrigin, Space.World);
            guard.transform.rotation = Quaternion.Euler(playerRot);
        }

    }

}
