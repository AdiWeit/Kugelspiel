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
    public void levelDone(int pLevel) {
      if (SystemInfo.deviceType == DeviceType.Handheld) {
        if (Input.gyro.enabled) {
          if (pLevel == 0) instructions.text = "Tilt your device like it is the box on the display and navigate the ball into the hole!";
        }
      }
      else {
        if (pLevel == 0) instructions.text = "Move the mouse to tilt the platform and navigate the ball into the hole! ";
        else if (pLevel == 4) instructions.text = "Don't let the big gray one block your hole! It has to come in last!";
        else if (pLevel != 1) instructions.text = "";
      }
      if (pLevel == 1) instructions.text = "Don't touch the red marble! So don't worry! It gets harder over time! ";
    }
    // for mobile debugging
    public void showText(string text) {
      instructions.text = text;
    }
}
