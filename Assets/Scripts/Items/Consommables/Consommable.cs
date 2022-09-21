using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consommable
{
    public Texture2D iconTexture;
    public string consommableName;
    public string consommableDescription;
    public int Id;
    public float statToModify;

    public Consommable(Texture2D icon, string name, string description, int id, float statModifier)
    {
        iconTexture = icon;
        consommableName = name;
        consommableDescription = description;
        Id = id;
        statToModify = statModifier;
    }
}

public class HealingPotion: Consommable
{
    public HealingPotion(Texture2D icon, string name, string description, int id, float statModifier) : base (icon, name, description, id, statModifier)
    {
        iconTexture = icon;
        consommableName = name;
        consommableDescription = description;
        Id = id;
        statToModify = statModifier;
    }
}

public class StrenghtPotion: Consommable
{
    public StrenghtPotion(Texture2D icon, string name, string description, int id, float statModifier) : base (icon, name, description, id, statModifier)
    {
        iconTexture = icon;
        consommableName = name;
        consommableDescription = description;
        Id = id;
        statToModify = statModifier;
    }
}
