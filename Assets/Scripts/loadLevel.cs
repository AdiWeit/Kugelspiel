using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int levelNr)
    {
      // name/string possible
      Debug.Log("opening scene " + "level_" + levelNr);
      SceneManager.LoadScene("level_" + levelNr);
    }
    public void beginEndlessRun()
    {
      SceneManager.LoadScene("endlessRunner");
    }
}
