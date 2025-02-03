using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCreationLogic : MonoBehaviour
{
  [SerializeField] private TMP_InputField inputField;

  public void CreateGame()
  {
    bool validName = false;

    foreach (char c in inputField.text.Where(c => c is not (' ' or '\t' or '\n' or '\r')))
    {
      validName = true;
      break;
    }

    if (!validName)
    {
      Debug.Log("Invalid name");
      return;
    }
    
    GameLogic.createMode = true;
    GameLogic.worldName = inputField.text;
    WorldsMeta.AddWorldMeta(inputField.text);
    Debug.Log(GameLogic.worldName);
    SceneManager.OpenGameScene();
  }
}