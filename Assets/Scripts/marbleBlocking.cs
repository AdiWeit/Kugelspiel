using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class marbleBlocking : MonoBehaviour
{
    public marbleAtGoal goalManager;
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
      // TODO
      if (other.gameObject.GetComponent<marbleParams>().type == "blocker") goalManager.goalReached(other.gameObject);
    }
}
