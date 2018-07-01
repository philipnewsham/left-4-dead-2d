using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public static class Slot
{
    public enum SlotType
    {
        Primary,
        Secondary,
        Throwable
    }
}

[System.Serializable]
public class Weapon
{
    [XmlAttribute("name")]
    public string weaponName;
    public Slot.SlotType slotType;
    public int maxAmmo;
    public int clipSize;
}
