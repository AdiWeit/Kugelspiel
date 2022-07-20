using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreSystem : MonoBehaviour
{
    private int score = 0;
    private Text textObj;
    // Start is called before the first frame update
    void Start()
    {
      textObj = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void goalReached()
    {
      score++;
      textObj.text = "Score: " + score;
    }
}
