using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openLevel_2 : MonoBehaviour
{
    public loadLevel levelLoader;
    // Start is called before the first frame update
    void Start()
    {
      gameObject.GetComponent<Button>().onClick.AddListener(clicked);
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void clicked()
    {
      levelLoader.LoadLevel(int.Parse(gameObject.name.Split(' ')[1]));
    }
}
