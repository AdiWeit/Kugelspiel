using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    private levelManager levelManager = new levelManager();
    private movePlane planeMovement = new movePlane();
    public GameObject[] level;
    private objectManager objectManager;
    void Start()
    {
      DontDestroyOnLoad(gameObject);
      if (GameObject.FindObjectsOfType<loadLevel>().Length > 1) Destroy(gameObject);
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int levelNr, bool pContinue)
    {
      Debug.Log("Try to open " + "level_" + levelNr);
      if (levelNr <= level.Length) {
        StartCoroutine(manageModeSelection(false, levelNr, levelManager.gameStarted, levelManager.sphereCount, planeMovement.startPosition, pContinue));
        SceneManager.LoadScene("level_scene");
        // StartCoroutine(levelLoaded(levelNr));
      }
      else {
        // TODO: handle
        Debug.Log("Level should be last one!");
      }
    }
    public void beginEndlessRun()
    {
      StartCoroutine(manageModeSelection(true, 1, levelManager.gameStarted, levelManager.sphereCount, planeMovement.startPosition, false));
      SceneManager.LoadScene("endlessRunner");
      levelManager.random = true;
    }
    public void loadSceneByString(string pString, bool pContinue)
    {
      SceneManager.LoadScene(pString);
      StartCoroutine(manageModeSelection(levelManager.random, levelManager.currentLevel, levelManager.gameStarted, levelManager.sphereCount, planeMovement.startPosition, pContinue));
    }
    IEnumerator levelLoaded(int levelNr)
    {
      yield return new WaitForSeconds(0.1f);
      Instantiate(level[levelNr - 1], new Vector3(446.5f, 302, 0), level[levelNr - 1].transform.rotation);
    }
    IEnumerator manageModeSelection(bool random, int levelNr, bool gameStarted, int pSphereCount, Vector3 pStartPosition, bool pContinue)
    {
      // yield return new WaitForSeconds(0.1f);
      yield return new WaitForSeconds(0.01f);
      Instantiate(level[levelNr - 1], new Vector3(446.5f, 302, 0), level[levelNr - 1].transform.rotation);
      planeMovement = GameObject.Find("movingCube").GetComponent<movePlane>();
      objectManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      objectManager.movingCube = planeMovement;
      Debug.Log("planeMovement: ");
      Debug.Log(planeMovement);
      if (!pContinue) {
        Debug.Log("startPosition: " + pStartPosition.ToString());
        planeMovement.startPosition = new Vector3(0, 0, 0); // pStartPosition;
        // levelManager.gameStarted = gameStarted;
      }
      else planeMovement.startPosition = pStartPosition;
      // levelManager.sphereCount = pSphereCount;
      if (!random && levelManager.gameStarted) GameObject.Find("gameInstructions").GetComponent<gameInstructions>().levelDone((levelNr - 1));
      if (Input.gyro.enabled && levelNr == 1) {
        GameObject.Find("gameInstructions").GetComponent<gameInstructions>().instructions.text = "Click on the screen to start the game. The box will instantly have the tilt your device has!";
      }
      yield return new WaitForSeconds(1);
      if (!random) {
        levelManager.startLevel(levelNr);
        if (levelManager.gameStarted)
        {
          foreach (GameObject marble in GameObject.FindGameObjectsWithTag("marble"))
          {
            marble.GetComponent<Rigidbody>().useGravity = true;
          }
        }
        levelManager.currentLevel = levelNr; 
      }
    }
}
