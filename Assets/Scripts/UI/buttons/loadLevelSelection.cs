using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadLevelSelection : MonoBehaviour
{
    private loadLevel levelLoader;
    // Start is called before the first frame update
    void Start()
    {
      // gameObject.GetComponent<Button>().onClick.AddListener(clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clicked()
    {
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
      if (levelLoader.random) {
        SceneManager.LoadScene("mainMenu");
        levelLoader.random = false;
      }
      else SceneManager.LoadScene("levelSelection");
    }
}