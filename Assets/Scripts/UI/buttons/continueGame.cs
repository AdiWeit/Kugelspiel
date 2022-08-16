using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueGame : MonoBehaviour
{
    private movePlane movePlane;
    public GameObject pauseMenu;
    public Material transparentRed;
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
        GameObject referencePlane = Instantiate(GameObject.Find("movingCube"), GameObject.Find("movingCube").transform.position, GameObject.Find("movingCube").transform.localRotation);
        referencePlane.GetComponent<MeshRenderer>().material = transparentRed;
        referencePlane.transform.Find("border").GetComponent<MeshRenderer>().material = transparentRed;
        for (int i = 0; i < referencePlane.transform.Find("border").childCount; i++) {
          Transform child = referencePlane.transform.Find("border").GetChild(i);
          child.GetComponent<MeshRenderer>().material = transparentRed;
        }
        movePlane.referencePlane = referencePlane;
      }
    }
}
