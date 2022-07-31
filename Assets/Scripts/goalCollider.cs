using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private levelManager levelManager;
    void Start()
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
      levelManager.goalReached(other.gameObject);
    }
}
