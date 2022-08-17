using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPreviewSpawner : MonoBehaviour
{
  public void spawnlevel(loadLevel levelLoader, int selectedLevel) {
    Instantiate(levelLoader.level[selectedLevel - 1], GameObject.Find("levelSpawner").transform.position, levelLoader.level[selectedLevel - 1].transform.rotation);
  }
}
