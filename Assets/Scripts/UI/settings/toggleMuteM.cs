using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleMuteM : MonoBehaviour
{
    public GameObject toggleMuteB;

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown("m")) toggleMuteB.GetComponent<toggleMute>().toggleMuteF(); 
    }
}
