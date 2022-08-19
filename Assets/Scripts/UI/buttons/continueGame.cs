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
        referencePlaneBorder.transform.position = movePlane.borderReference.transform.position;
        movePlane.referencePlane = referencePlaneBorder;
      }
      else {
        if (SystemInfo.supportsGyroscope && movePlane.startPosition == new Vector3(0, 0, 0)) {
          movePlane.startPosition = new Vector3(Screen.width/2, Screen.height/2, 0); // new Vector3(427, 307, 0);
        }
        GameObject.Find("mouseBackCircle").transform.localPosition = movePlane.getMousePosition(movePlane.startPosition + movePlane.rotationPosition/movePlane.sensitivity);//;
      }
    }
}
