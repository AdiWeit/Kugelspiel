using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openLevel : MonoBehaviour
{
    public loadLevel levelLoader;
    public levelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
      gameObject.GetComponent<Button>().onClick.AddListener(clicked);
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void clicked()
    {
      levelManager.gameStarted = false;
      levelLoader.LoadLevel(int.Parse(gameObject.name.Split(' ')[1]), false);
    }
}
