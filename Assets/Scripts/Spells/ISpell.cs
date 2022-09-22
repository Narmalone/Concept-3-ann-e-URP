using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    //fonction qui permet de lancer un sort n'importe lequel 
    //appelée dans une classe ou y'a un SO
    //tableau de ISpell pour récupérer
    public void CastSpell();

    public void CastEffect();
}
