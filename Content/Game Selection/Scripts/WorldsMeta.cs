using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class WorldsMeta
{
  public List<string> worldNames = new();

  private void Save()
  {
    if (!File.Exists(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml"))
    {
      File.Create(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml").Close();
    }
    
    XmlSerializer serializer = new XmlSerializer(typeof(WorldsMeta));
    XmlWriter xmlWriter = XmlWriter.Create(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml");
    
    serializer.Serialize(xmlWriter, this);
    
    xmlWriter.Close();
  }
  
  public static List<string> ReadWorldNames()
  {
    WorldsMeta worldsMeta = new WorldsMeta(); // create new WorldsMeta class instance
    
    XmlSerializer serializer = new XmlSerializer(typeof(WorldsMeta));
    
    // create WorldsMeta.xml file and format it if it doesn't already exist
    if (!File.Exists(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml"))
    {
      worldsMeta.Save();
    }
    
    // read from the WorldsMeta.xml file
    XmlReader xmlReader = XmlReader.Create(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml");
    worldsMeta = (WorldsMeta)serializer.Deserialize(xmlReader);
    xmlReader.Close();
    
    return worldsMeta.worldNames;
  }

  public static void AddWorldMeta(string worldName)
  {
    WorldsMeta worldsMeta = new WorldsMeta(); // create new WorldsMeta class instance
    
    XmlSerializer serializer = new XmlSerializer(typeof(WorldsMeta));
    
    // create WorldsMeta.xml file and format it if it doesn't already exist
    if (!File.Exists(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml"))
    {
      worldsMeta.Save();
    }
    
    // read from the WorldsMeta.xml file
    XmlReader xmlReader = XmlReader.Create(@"Executable\Pixels_Data\Worlds\WorldsMeta.xml");
    worldsMeta = (WorldsMeta)serializer.Deserialize(xmlReader);
    xmlReader.Close();
    
    worldsMeta.worldNames.Add(worldName);
    
    worldsMeta.Save();
  }
}