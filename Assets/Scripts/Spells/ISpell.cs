using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    //fonction qui permet de lancer un sort n'importe lequel 
    //appel�e dans une classe ou y'a un SO
    //tableau de ISpell pour r�cup�rer
    public void CastSpell();

    public void CastEffect();
}
