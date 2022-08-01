using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueGame : MonoBehaviour
{
    private levelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void continueGameF()
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      Debug.Log("reopen " + levelManager.sceneBefore);
      Debug.Log("levelManager: " + levelManager.levelLoader);
      levelManager.levelLoader.loadSceneByString(levelManager.sceneBefore, true);
    }
}
