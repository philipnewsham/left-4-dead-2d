using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("WeaponCollection")]
public class WeaponContainer
{
    [XmlArray("Weapons")]
    [XmlArrayItem("Weapon")]
    public List<Weapon> weapons = new List<Weapon>();

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(WeaponContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static WeaponContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(WeaponContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as WeaponContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static WeaponContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(WeaponContainer));
        return serializer.Deserialize(new StringReader(text)) as WeaponContainer;
    }
}
