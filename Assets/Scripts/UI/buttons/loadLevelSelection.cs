using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadLevelSelection : MonoBehaviour
{
    private levelManager levelManager;
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
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      if (levelManager.random) {
        SceneManager.LoadScene("mainMenu");
        levelManager.random = false;
      }
      else SceneManager.LoadScene("levelSelection");
      Time.timeScale = 1f;
    }
}