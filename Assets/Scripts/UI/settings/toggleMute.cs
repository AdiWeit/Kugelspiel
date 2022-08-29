using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class toggleMute : MonoBehaviour
{
  public Sprite mutedSymbol;
  public Sprite unmutedSymbol;
  public GameObject settingsManager;
  public void toggleMuteF()
  {
    settingsManager.GetComponent<settingsManager>().setMuted(!settingsManager.GetComponent<settingsManager>().isMuted);
    if (settingsManager.GetComponent<settingsManager>().isMuted)
    {
      gameObject.GetComponent<UnityEngine.UI.Image>().sprite = mutedSymbol;
    }
    else gameObject.GetComponent<UnityEngine.UI.Image>().sprite = unmutedSymbol;
  }
}
