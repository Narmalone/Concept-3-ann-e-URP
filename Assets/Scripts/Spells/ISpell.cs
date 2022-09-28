using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{

    public string Name { get;}
    public string Description { get;}
    public int Cost { get; set; }
    public int GeneralId { get; set; }
    public float Damage { get; set; }
    public Texture2D Icon { get; }
    public bool IsEnemyTarget { get; }
    public bool IsSoloTarget { get; }

    //fonction qui permet de lancer un sort n'importe lequel 
    //appelée dans une classe ou y'a un SO
    //tableau de ISpell pour récupérer
    public void CastSpell(Spell spell);
}
