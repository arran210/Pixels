using TMPro;
using UnityEngine;

public class SaveFileInterface : MonoBehaviour
{
  public string saveFileName { get; private set; }
  [SerializeField] private TMP_Text title;

  public void Init(string saveFileName)
  {
    this.saveFileName = saveFileName;
    gameObject.name = saveFileName;
    title.text = saveFileName;
  }

  public void Load()
  {
    GameLogic.worldName = saveFileName;
    GameLogic.createMode = false;
    SceneManager.OpenGameScene();
  }
}