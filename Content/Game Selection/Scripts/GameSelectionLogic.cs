using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameSelectionLogic : MonoBehaviour
{
  [SerializeField] private Transform saveFileInterfaceTransform;
  [SerializeField] private GameObject saveFileInterfacePrefab;
  [SerializeField] private List<GameObject> saveFileInterfaces;
  private void Awake()
  {
    WorldsMeta worldsMeta = new WorldsMeta(); // create new WorldsMeta class instance
    
    XmlSerializer serializer = new XmlSerializer(typeof(WorldsMeta));
    
    if (!File.Exists(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml"))
    // create WorldsMeta.xml file and format it if it doesn't already exist
    {
      File.Create(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml").Close();
      
      using FileStream fileStream = new FileStream(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml", FileMode.Create);
      serializer.Serialize(fileStream, worldsMeta);
    }
    
    StreamReader reader = new StreamReader(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml");
    
    worldsMeta = (WorldsMeta)serializer.Deserialize(reader);
    reader.Close();
    
    float saveFileInterfaceHeight = saveFileInterfacePrefab.GetComponent<RectTransform>().rect.height;
    
    for (int i = 0; i < worldsMeta.worldNames.Count; i++)
    {
      saveFileInterfaces.Add(Instantiate(saveFileInterfacePrefab, saveFileInterfaceTransform));
      saveFileInterfaces.Last().transform.localPosition = new Vector2(0, -i * (saveFileInterfaceHeight + 10));
      saveFileInterfaces.Last().GetComponent<SaveFileInterface>().Init(worldsMeta.worldNames[i]);
    }
  }

  private void Update()
  {
    if (Input.mouseScrollDelta.y != 0)
    {
      float saveFileInterfaceHeight = saveFileInterfacePrefab.GetComponent<RectTransform>().rect.height;
      
      saveFileInterfaceTransform.position += new Vector3(0, Input.mouseScrollDelta.y * -20);

      if (saveFileInterfaceTransform.position.y < Screen.height - 10) saveFileInterfaceTransform.position = new Vector3(saveFileInterfaceTransform.position.x, Screen.height - 10);
      if (saveFileInterfaceTransform.position.y > (saveFileInterfaceHeight + 10) * saveFileInterfaces.Count) saveFileInterfaceTransform.position = new Vector3(saveFileInterfaceTransform.position.x, (saveFileInterfaceHeight + 10) * saveFileInterfaces.Count);
    }
  }
}