using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreSystem : MonoBehaviour
{
    private int score { get; set; }
    private Text textObj;
    // Start is called before the first frame update
    void Start()
    {
      score = 0;
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
