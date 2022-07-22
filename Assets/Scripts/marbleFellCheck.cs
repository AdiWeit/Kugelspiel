using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marbleFellCheck : MonoBehaviour
{
    public liveManager liveManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (gameObject.transform.position.y < -3.15) {
        Debug.Log("Marble fell down!");
        liveManager.takeDamage(false);
        gameObject.transform.position = new Vector3(0, 10, 0);
      }
    }
}
