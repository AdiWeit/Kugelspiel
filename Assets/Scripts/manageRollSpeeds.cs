using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageRollSpeeds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      gameObject.GetComponent<Rigidbody>().maxAngularVelocity = 33;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
