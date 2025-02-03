using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCreationLogic : MonoBehaviour
{
  [SerializeField] private TMP_InputField inputField;

  public void CreateGame()
  {
    if (inputField.text == "")
    {
      Debug.Log("World Name cannot be empty");
      return;
    }
    
    GameLogic.createMode = true;
    GameLogic.worldName = inputField.text;
    Debug.Log(GameLogic.worldName);
    SceneManager.OpenGameScene();
  }
}