using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameInstructions : MonoBehaviour
{
    private Text instructions;
    // Start is called before the first frame update
    void Start()
    {
      instructions = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void levelDone(int pLevel) {
      if (pLevel == 0) instructions.text = "move the mouse to tilt the platform and navigate the ball into the hole! ";
      if (pLevel == 1) instructions.text = "don't worry! It gets harder over time!";
      if (pLevel > 2) instructions.text = "";
    }
}
