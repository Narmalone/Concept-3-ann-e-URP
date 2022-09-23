using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{

    //So appelé dans une classe ou y'a besoin d'un sort
    public string Name { get;}
    public string Description { get; set;}
    public int GeneralId { get;}
    public float Damage { get;}
    public Texture2D Icon { get; }

    //fonction qui permet de lancer un sort n'importe lequel 
    //appelée dans une classe ou y'a un SO
    //tableau de ISpell pour récupérer
    public void CastSpell();
}
