using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    private levelManager levelManager = new levelManager();
    private movePlane planeMovement = new movePlane();
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
      Debug.Log("Try to open scene " + "level_" + levelNr);
      if (SceneUtility.GetBuildIndexByScenePath("level_" + levelNr) >= 0) {
        StartCoroutine(manageModeSelection(false, levelNr, levelManager.gameStarted, levelManager.sphereCount, planeMovement.startPosition));
        SceneManager.LoadScene("level_" + levelNr);
      }
      else {
        // TODO: handle
        Debug.Log("Level should be last one!");
      }
    }
    public void beginEndlessRun()
    {
      StartCoroutine(manageModeSelection(true, 1, levelManager.gameStarted, levelManager.sphereCount, planeMovement.startPosition));
      SceneManager.LoadScene("endlessRunner");
    }
    IEnumerator manageModeSelection(bool random, int levelNr, bool gameStarted, int pSphereCount, Vector3 pStartPosition)
    {
      // yield return new WaitForSeconds(0.1f);
      yield return new WaitForSeconds(0.01f);
      levelManager = GameObject.Find("goal_hitbox").GetComponent<levelManager>();
      planeMovement = GameObject.Find("movingCube").GetComponent<movePlane>();
      planeMovement.startPosition = pStartPosition;
      // set startPosition
      levelManager.gameStarted = gameStarted;
      levelManager.sphereCount = pSphereCount;
      if (!random && levelManager.gameStarted) GameObject.Find("gameInstructions").GetComponent<gameInstructions>().levelDone((levelNr - 1));
      if (Input.gyro.enabled && levelNr == 1) {
        GameObject.Find("gameInstructions").GetComponent<gameInstructions>().instructions.text = "Click on the screen to start the game. The box will instantly have the tilt your device has!";
      }
      yield return new WaitForSeconds(1);
      levelManager.random = random;
      if (!random) {
        if (levelManager.gameStarted)
        {
          levelManager.startLevel(levelNr);
          foreach (GameObject marble in GameObject.FindGameObjectsWithTag("marble"))
          {
            marble.GetComponent<Rigidbody>().useGravity = true;
          }
        }
        levelManager.currentLevel = levelNr; 
      }
    }
}
