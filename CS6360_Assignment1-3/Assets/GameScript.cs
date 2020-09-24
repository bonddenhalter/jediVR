using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for Text

public class GameScript : MonoBehaviour
{

    public float timeSinceLastSwitch;
    public Text scoreboard;
    public GameObject oldBox;

    private string[] boxNames = { "Box 1", "Box 2", "Box 3", "Box 4" };
    private const int ON_INTENSITY = 10;
    private uint points = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSwitch = 0;
        SwitchLightRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update(); //needed to get Oculus controller input

        if (Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Two))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


        timeSinceLastSwitch += Time.deltaTime;
        if (timeSinceLastSwitch > 3.0f)
        {
            SwitchLightRandomly();
            timeSinceLastSwitch = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate(); //needed to get Oculus controller input
    }

    public void SwitchLightRandomly() 
    {
 
        //turn all lights off
        foreach (string boxName in boxNames)
        {
            
            GameObject box = GameObject.Find(boxName);
            if (box.GetComponentInChildren<Light>().intensity != 0)
            {
                oldBox = box;
            }
            Light light = box.GetComponentInChildren<Light>();
            light.intensity = 0;
        }
        GameObject randomBox = GameObject.Find(boxNames[Random.Range(0, boxNames.Length)]);
        //turn on random light
        while (randomBox == oldBox)
        {
            randomBox = GameObject.Find(boxNames[Random.Range(0, boxNames.Length)]);
        }
        Light randomlight = randomBox.GetComponentInChildren<Light>();
        randomlight.intensity = ON_INTENSITY;
    }

    public void ScorePoint()
    {
        points++;
        scoreboard.text = "Score: " + points;
    }
}
