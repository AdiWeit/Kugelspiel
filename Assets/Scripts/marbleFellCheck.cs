using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class marbleFellCheck : MonoBehaviour
{
    public liveManager liveManager;
    // Start is called before the first frame update
    void Start()
    {
      liveManager = GameObject.Find("liveManager").GetComponent<liveManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if (SceneManager.GetActiveScene().name == "levelSelection") return;
      if (gameObject.transform.position.y < -3.15) {
        Debug.Log("Marble fell down!");
        liveManager.takeDamage(false);
        gameObject.transform.position = new Vector3(0, 10, 0);
      }
    }
}
