using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{

    public Light light;
    uint curColorIndex = 0;
    Color[] colors = { Color.cyan, Color.yellow, Color.green, Color.red, Color.blue, Color.white, Color.magenta };

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("tab"))
        {
            if(++curColorIndex >= colors.Length)
            {
                curColorIndex = 0;
            }
            light.color = colors[curColorIndex];
        }
    }
}
