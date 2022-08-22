using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json; // on the top of the file.
using UnityEngine.UI;

public class loadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    private levelManager levelManager; // = new levelManager();
    private movePlane planeMovement = new movePlane();
    public GameObject[] level;
    public GameObject endlessRunnerPref;
    public objectManager objectManager;
    public settingsManager settingsManager;
    public GameObject triesManager;
    public Material greenGlass;
    public bool levelSelected = false;
    void Start()
    {
      DontDestroyOnLoad(gameObject);
      if (GameObject.FindObjectsOfType<loadLevel>().Length > 1) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int levelNr, bool pContinue, bool restartLevel)
    {
      Debug.Log("Try to open " + "level_" + levelNr);
      if (levelNr <= level.Length) {
        StartCoroutine(manageModeSelection(false, restartLevel, levelNr, planeMovement.startPosition, pContinue));
        // StartCoroutine(levelLoaded(levelNr));
      }
      else {
        Debug.Log("Level should be last one!");
        SceneManager.LoadScene("levelSelection");
      }
    }
    public void beginEndlessRun()
    {
      StartCoroutine(manageModeSelection(true, false, 1, planeMovement.startPosition, false));
      Debug.Log("begin endless run");
    }
    public void openLevelSelection() {
      SceneManager.LoadScene("levelSelection");
      StartCoroutine(corLoadHighscores());
    }
    public IEnumerator corLoadHighscores() {
      yield return new WaitForSeconds(0.1f); 
      loadHighscores(true);
    }
    public void loadHighscores(bool display) {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      // levelManager.highscores = JsonConvert.DeserializeObject<bool[]>(PlayerPrefs.GetString("levelHighscores"));
      string[] convertList = PlayerPrefs.GetString("levelHighscores").Replace("[", "").Replace("]", "").Split(",");
      Debug.Log("convertList: ");
      Debug.Log(convertArrayToString(convertList));
      if (convertList[0] == "") convertList = popArrayInxed(convertList);
      for (int i = 0; i < convertList.Length; i++)
      {
        Debug.Log(convertList[i]);
        // if (levelManager.highscores.Length - 1 < i) {
        if (convertList[i] != "") {
          Debug.Log("old highscores: " + levelManager.convertArrayToString(levelManager.highscores));
          levelManager.highscores = levelManager.setArrayIndexValue(bool.Parse(convertList[i]), i);
        }
        // }
        // else {
        //   levelManager.highscores[i] = bool.Parse(convertList[i]);
        // }
        if (levelManager.highscores[i] && display) GameObject.Find("level " + i).GetComponent<Image>().color = greenGlass.color;
      }
      // levelManager.highscores = Array.parse(PlayerPrefs.GetString());
    }
    public string[] popArrayInxed(string[] array) {
      string[] newArray = new string[array.Length - 1];
      for (int i = 1; i < newArray.Length; i++)
      {
        newArray[i - 1] = array[i];
      }
      return newArray;
    }
    public string convertArrayToString (string[] array) {
      string pString = "[";
      for (int i = 0; i < array.Length; i++)
      {
        pString += array[i];
        if (i + 1 != array.Length) pString += ",";
      }
      pString += "]";
      return pString;
    }
    public IEnumerator manageModeSelection(bool random, bool restartLevel, int levelNr, /*bool gameStarted, int pSphereCount, */Vector3 pStartPosition, bool pContinue)
    {
      if (!restartLevel && !pContinue) SceneManager.LoadScene("level_scene");
      else {
        if (random) Destroy(GameObject.Find("endlessRunner(Clone)"));
        else {
          if (pContinue) Destroy(GameObject.Find("level_" + (levelNr - 1) + "(Clone)"));
          Destroy(GameObject.Find("level_" + levelNr + "(Clone)"));
        }
      }
      // yield return new WaitForSeconds(0.1f);
      yield return new WaitForSeconds(0.01f);
      if (random) Instantiate(endlessRunnerPref, new Vector3(446.5f, 302, 0), endlessRunnerPref.transform.rotation);
      else Instantiate(level[levelNr - 1], new Vector3(446.5f, 302, 0), level[levelNr - 1].transform.rotation);
      yield return new WaitForSeconds(0.01f);
      if (pContinue && GameObject.Find("settingsManager").GetComponent<settingsManager>().resetBoxPosition) {
        levelManager.gameStarted = false;
        GameObject.Find("movingCube").transform.eulerAngles = new Vector3(0, 0, 0);
        if (Input.gyro.enabled) {
          GameObject.Find("playBReference").GetComponent<playBReference>().playB.GetComponent<continueGame>().continueGameF();
          planeMovement.instructionsText.showText("tilt your device like it is lying on a table, so the red indication border has to be in the same angle the box is. ");
        }
      }
      planeMovement = GameObject.Find("movingCube").GetComponent<movePlane>();
      objectManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      objectManager.movingCube = planeMovement;
      if (!pContinue) {
        Debug.Log("startPosition: " + pStartPosition.ToString());
        planeMovement.startPosition = new Vector3(0, 0, 0); // pStartPosition;
      }
      else if (!GameObject.Find("settingsManager").GetComponent<settingsManager>().resetBoxPosition) planeMovement.startPosition = pStartPosition;
      // if (!random && levelManager.gameStarted) GameObject.Find("gameInstructions").GetComponent<gameInstructions>().levelDone((levelNr - 1));
      if (Input.gyro.enabled && levelNr == 1) {
        GameObject.Find("gameInstructions").GetComponent<gameInstructions>().instructions.text = "Click on the screen to start the game. The box will instantly have the tilt your device has!";
      }
      if (random) {
        objectManager.spawnSphere(0, 0, "normal");
        GameObject.Find("livesTextReference").GetComponent<livesTextReference>().livesText.SetActive(true);
      }
      else {
        GameObject.Find("triesTextReference").GetComponent<triesTextReference>().triesText.SetActive(true);
        if (!Input.gyro.enabled && restartLevel && GameObject.Find("settingsManager").GetComponent<settingsManager>().resetBoxPosition) planeMovement.instructionsText.showText("Click to place a new joystick. Move your mouse/finger to use the joystick. ");
        if (restartLevel) {
          triesManager = GameObject.Find("triesManager");
          triesManager.GetComponent<triesManager>().resetTries();
        }
      }
      settingsManager = GameObject.Find("settingsManager").GetComponent<settingsManager>();
      settingsManager.getSettings(false);
      yield return new WaitForSeconds(1);
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      levelManager.random = random;
        if (!random) {
        levelManager.startLevel(levelNr, false);
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
