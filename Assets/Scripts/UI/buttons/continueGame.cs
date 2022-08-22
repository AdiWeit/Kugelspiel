using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class continueGame : MonoBehaviour
{
    private movePlane movePlane;
    public GameObject pauseMenu;
    public Material transparentRed;
    public GameObject referencePlaneBorder;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void continueGameF()
    {
      movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
      pauseMenu.SetActive(false);
      movePlane.waitForMousePosition = true;
      if (Input.gyro.enabled) {
        Vector3 borderPos = movePlane.borderReference.transform.position;
        borderPos.y = 0.97f;
        referencePlaneBorder.transform.position = borderPos;
        movePlane.referencePlane = referencePlaneBorder;
        movePlane.instructionsText.showText("tilt your device so the red indication border is in the same angle the box is. This way, you are back to the position you paused with. Alternatively, click two times (doubleclick possible) to start the level with your current tilt.");
      }
      else {
        movePlane.instructionsText.showText("click on the red point to continue the game with the same box tilt you started with. ");
        if (SystemInfo.supportsGyroscope && movePlane.startPosition == new Vector3(0, 0, 0)) {
          movePlane.startPosition = new Vector3(Screen.width/2, Screen.height/2, 0); // new Vector3(427, 307, 0);
        }
        GameObject.Find("mouseBackCircle").transform.localPosition = movePlane.getMousePosition(movePlane.startPosition + movePlane.rotationPosition/movePlane.sensitivity);//;
      }
    }
}
