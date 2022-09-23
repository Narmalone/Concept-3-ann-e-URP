using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{

    public string Name { get;}
    public string Description { get; set;}
    public int GeneralId { get;}
    public float Damage { get;}
    public Texture2D Icon { get; }
    public bool IsEnemyTarget { get; }
    public bool IsSoloTarget { get; }

    //fonction qui permet de lancer un sort n'importe lequel 
    //appel�e dans une classe ou y'a un SO
    //tableau de ISpell pour r�cup�rer
    public void CastSpell(Spell spell);
}
