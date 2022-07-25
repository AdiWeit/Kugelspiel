using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startEndlessMode : MonoBehaviour
{
    public loadLevel levelLoader;
    // Start is called before the first frame update
    void Start()
    {
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
      gameObject.GetComponent<Button>().onClick.AddListener(clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void clicked()
    {
      levelLoader.beginEndlessRun();
    }
}
