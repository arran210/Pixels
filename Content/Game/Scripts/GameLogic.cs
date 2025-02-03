using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using System.IO;

public class GameLogic : MonoBehaviour
{
  public static string worldName; // must be set before scene loads
  public static bool createMode; // must be set before scene loads

  private static void Create()
  {
    string worldNameHash = ComputeSHA256Hash(worldName);
    if (Directory.Exists(@$"Executable\Pixels_Data\Worlds\{worldNameHash}"))
    {
      Debug.Log("Create Directory already exists");
      SceneManager.OpenTitleScene();
      return;
    }
    Directory.CreateDirectory(@$"Executable\Pixels_Data\Worlds\{worldNameHash}");
    File.Create(@$"Executable\Pixels_Data\Worlds\{worldNameHash}\World.xml").Close();
    File.Create(@$"Executable\Pixels_Data\Worlds\{worldNameHash}\Player.xml").Close();
  }
  
  public static void Save()
  {
    string worldNameHash = ComputeSHA256Hash(worldName);
    if (!Directory.Exists(@$"Executable\Pixels_Data\Worlds\{worldNameHash}")) return;
    
  }

  private static void Load()
  {
    string worldNameHash = ComputeSHA256Hash(worldName);
    if (Directory.Exists(@$"Executable\Pixels_Data\Worlds\{worldNameHash}"))
    {
      Debug.Log("Load Directory exists");
    }
    else
    {
      Debug.Log("Load Directory does not exist");
      Debug.Log(worldNameHash);
      SceneManager.OpenTitleScene();
    }
  }

  private void Awake()
  {
    if (createMode)
    {
      Create();
    }
    else
    {
      Load();
    }
  }

  private static string ComputeSHA256Hash(string input)
  {
    if (input == null) return "NULL";
    using SHA256 sha256 = SHA256.Create();
    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
    byte[] hashBytes = sha256.ComputeHash(inputBytes);

    StringBuilder sb = new StringBuilder();
    foreach (byte b in hashBytes)
    {
      sb.Append(b.ToString("x2"));
    }
    return sb.ToString();
  }
}