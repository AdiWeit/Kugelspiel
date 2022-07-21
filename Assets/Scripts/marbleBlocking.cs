using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class marbleBlocking : MonoBehaviour
{
    public levelManager goalManager;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.GetComponent<marbleParams>().type == "blocker") goalManager.goalReached(other.gameObject);
    }
}
