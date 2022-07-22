using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marbleKilledCheck : MonoBehaviour
{
    public liveManager liveManager;
    public marbleParams marbleParams;
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
      if (other.gameObject.GetComponent<marbleParams>()?.type == "enemy" || (other.gameObject.GetComponent<marbleParams>()?.type == "breaking" && (other.gameObject.GetComponent<marbleParams>().speed > 0.5 || gameObject.GetComponent<marbleParams>().speed > 0.5)))
      {
        Debug.Log("Restart level because enemy touched marble or marble collided too hard!");
        liveManager.takeDamage(false);
      }
    }
}
