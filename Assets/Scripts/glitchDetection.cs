using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitchDetection : MonoBehaviour
{
    public objectManager objManager;
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
      Debug.Log("sphere glitched! new one is spawning!");
      objManager.spawnSphere(-0.52f, -0.5f, other.gameObject.GetComponent<marbleParams>().type);
      Destroy(other.gameObject);
    }
}