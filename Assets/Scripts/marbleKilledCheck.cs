using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marbleKilledCheck : MonoBehaviour
{
    public liveManager liveManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.GetComponent<marbleParams>()?.type == "enemy")
      {
        Debug.Log("Restart level because enemy touched marble!");
        liveManager.takeDamage();
      }
    }
}
