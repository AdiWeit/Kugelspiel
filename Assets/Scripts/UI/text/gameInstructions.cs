using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameInstructions : MonoBehaviour
{
    public Text instructions;
    // Start is called before the first frame update
    void Start()
    {
      instructions = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void levelDone(int pLevel, bool random) {
      if (random) pLevel++;
      instructions.text = "";
      if (SystemInfo.deviceType == DeviceType.Handheld) {
        if (Input.gyro.enabled) {
          if (pLevel == 1) instructions.text = "Tilt your device like it is the box on the display and navigate the ball into the hole!";
        }
        else instructions.text = "Hold your finger on the screen and move it in the direction you want the box to be to navigate the ball into the hole! ";
      }
      else {
        if (pLevel == 1) instructions.text = "Move the mouse to tilt the platform and navigate the ball into the hole! ";
      }
        if ((random && pLevel == 2) || (!random && pLevel == 16)) instructions.text = "Don't touch the red marble! So don't worry! It gets harder over time! ";
        if ((random && pLevel == 5) || (!random && pLevel == 13)) instructions.text = "Don't let the big gray one block your hole! It has to come in last!";
        if ((random && pLevel == 6) || (!random && pLevel == 14)) instructions.text = "Blue marbles roll faster!";
        if ((random && pLevel == 7)) instructions.text = "Orange marbles bounce, so they can overcome the borders. Always have an eye on them!";
    }
    // for mobile debugging
    public void showText(string text) {
      instructions.text = text;
    }
}
