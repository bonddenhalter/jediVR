using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxLightScript : MonoBehaviour
{
    public GameScript gameScript;

    private bool buttonAPrev = false;

    private void OnTriggerEnter(Collider other)
    {
        print("entered " + this.name);        
    }

    private void OnTriggerStay(Collider other)
    {
        bool buttonA = OVRInput.Get(OVRInput.Button.One);

        if(buttonA && !buttonAPrev)
        {
            print("A pressed in " + this.name);
            if (GetComponentInChildren<Light>().intensity > 0)
            {
                gameScript.ScorePoint();
                gameScript.SwitchLightRandomly();
            }
        }

        buttonAPrev = buttonA;
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    }




}
