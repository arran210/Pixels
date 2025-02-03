using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
  public static SceneManager instance;

  private void Awake()
  {
    instance = this;
  }
  
  public static void OpenTitleScene()
  {
    // unloads all active scenes and opens the game scene
    UnitySceneManager.LoadScene("Title", LoadSceneMode.Single);
  }

  public static void OpenGameSelectionScene()
  {
    // unloads all active scenes and opens the game selection scene
    UnitySceneManager.LoadScene("Game Selection", LoadSceneMode.Single);
  }

  public static void OpenGameCreationScene()
  {
    // unloads all active scenes and opens the game creation scene
    UnitySceneManager.LoadScene("Game Creation", LoadSceneMode.Single);
  }
  
  public static void OpenGameScene()
  {
    // unloads all active scenes and opens the title scene
    UnitySceneManager.LoadScene("Game", LoadSceneMode.Single);
  }
  
  public void OpenPauseScene()
  {
    UnitySceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    
    StartCoroutine(SetActiveSceneAsync("Pause"));
  }

  public static void ClosePauseScene()
  {
    // start unloading pause scene
    UnitySceneManager.UnloadSceneAsync("Pause");
    
    // decide which scene to hand control to
    if (UnitySceneManager.GetSceneByName("Game").isLoaded)
    {
      UnitySceneManager.SetActiveScene(UnitySceneManager.GetSceneByName("Game"));
    }
    else if (UnitySceneManager.GetSceneByName("Title").isLoaded)
    {
      UnitySceneManager.SetActiveScene(UnitySceneManager.GetSceneByName("Title"));
    }
  }

  public void OpenSettingsScene()
  {
    UnitySceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    StartCoroutine(SetActiveSceneAsync("Settings"));
  }

  public static void CloseSettingsScene()
  {
    // start unloading settings scene
    UnitySceneManager.UnloadSceneAsync("Settings");
    
    // decide which scene to hand control to
    if (UnitySceneManager.GetSceneByName("Pause").isLoaded)
    {
      UnitySceneManager.SetActiveScene(UnitySceneManager.GetSceneByName("Pause"));
    }
    else if (UnitySceneManager.GetSceneByName("Game").isLoaded)
    {
      UnitySceneManager.SetActiveScene(UnitySceneManager.GetSceneByName("Game"));
    }
    else if (UnitySceneManager.GetSceneByName("Title").isLoaded)
    {
      UnitySceneManager.SetActiveScene(UnitySceneManager.GetSceneByName("Title"));
    }
  }

  public static void ExitGame()
  {
    Application.Quit();
  }

  private static IEnumerator SetActiveSceneAsync(string sceneName)
  {
    const float timeOut = 5f; // time out in seconds
    float timer = 0f;

    while (timer < timeOut)
    {
      timer += Time.deltaTime;
      
      Scene targetScene = UnitySceneManager.GetSceneByName(sceneName);

      if (targetScene.IsValid() && targetScene.isLoaded)
      {
        UnitySceneManager.SetActiveScene(targetScene);
        yield break;
      }

      yield return null;
    }

    Debug.LogError($"Loading scene {sceneName} timed out after {timer} seconds.");
  }
}
