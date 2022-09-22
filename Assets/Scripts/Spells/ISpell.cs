using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    public void CastSpell() { }
    public void CreateSpell(Spells newSpell, Character target);
    public void CreateSpell(Spells newSpell);

    public void AttributeRandomSpell(Spells randomSpell);
    public void AttributeRandomSpell(Spells randomSpell, Character target);
}
