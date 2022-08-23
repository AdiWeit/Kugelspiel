using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
      if (pContinue && !restartLevel && GameObject.Find("settingsManager").GetComponent<settingsManager>().skipLevels) {
        Destroy(GameObject.Find("level_" + (levelNr - 1) + "(Clone)"));
        while (levelNr < levelManager.highscores.Length && levelManager.highscores[levelNr]) {
          levelNr++;
        }
        if (levelNr >= level.Length) {
          openLevelSelection();
          return;
        }
      }
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
      GameObject.Find("randomSettingsManager").GetComponent<endlessRunnerSettingsManager>().getSettings();
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
      string[] convertList = PlayerPrefs.GetString("levelHighscores").Replace("[", "").Replace("]", "").Split(",");
      for (int i = 1; i < convertList.Length; i++)
      {
        if (convertList[i] != "") {
          levelManager.highscores = levelManager.setArrayIndexValue(bool.Parse(convertList[i]), i);
        }
        if (levelManager.highscores[i] && display) GameObject.Find("level " + i).GetComponent<Image>().color = greenGlass.color;
      }
    }
    public IEnumerator manageModeSelection(bool random, bool restartLevel, int levelNr, /*bool gameStarted, int pSphereCount, */Vector3 pStartPosition, bool pContinue)
    {
      if (!restartLevel && !pContinue && SceneManager.GetActiveScene().name != "level_scene") SceneManager.LoadScene("level_scene");
      else {
        if (random) Destroy(GameObject.Find("endlessRunner(Clone)"));
        else {
          if (pContinue) Destroy(GameObject.Find("level_" + (levelNr - 1) + "(Clone)"));
          Destroy(GameObject.Find("level_" + levelNr + "(Clone)"));
        }
      }
      // yield return new WaitForSeconds(0.1f);
      yield return new WaitForSeconds(0.01f);
      if (GameObject.Find("endlessSettingsMenu") != null) GameObject.Find("endlessSettingsMenu").SetActive(false);
      GameObject.Find("levelUIReference").GetComponent<levelUIRef>().levelUI.SetActive(true);
      if (random) Instantiate(endlessRunnerPref, new Vector3(446.5f, 302, 0), endlessRunnerPref.transform.rotation);
      else Instantiate(level[levelNr - 1], new Vector3(446.5f, 302, 0), level[levelNr - 1].transform.rotation);
      yield return new WaitForSeconds(0.01f);
      if (pContinue && GameObject.Find("settingsManager").GetComponent<settingsManager>().resetBoxPosition) {
        levelManager.gameStarted = false;
        GameObject.Find("movingCube").transform.eulerAngles = new Vector3(0, 0, 0);
        if (Input.gyro.enabled) {
          GameObject.Find("playBReference").GetComponent<playBReference>().playB.GetComponent<continueGame>().continueGameF();
          planeMovement.instructionsText.showText("tilt your device like it is lying on a table, so the red indication border has to be in the same angle the box is. Alternatively, click two times (doubleclick possible) to start the level with your current tilt. You can also turn this off in the settings.");
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
      if (!PlayerPrefs.HasKey("useGyro") || PlayerPrefs.GetInt("useGyro") == 1) {
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
      yield return new WaitForSeconds(1);
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      levelManager.random = random;
      settingsManager.getSettings(false);
        if (!random) {
        levelManager.startLevel(levelNr, false);
        if (levelManager.gameStarted)
        {
          foreach (GameObject marble in GameObject.FindGameObjectsWithTag("marble"))
          {
            marble.GetComponent<SphereCollider>().isTrigger = false;
            marble.GetComponent<Rigidbody>().useGravity = true;
          }
        }
        levelManager.currentLevel = levelNr; 
      }
    }
}
