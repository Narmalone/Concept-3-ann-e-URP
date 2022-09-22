using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpells
{
    public void CreateSpell(Spells newSpell, Character target);
    public void CreateSpell(Spells newSpell);

    public void AttributeRandomSpell(Spells randomSpell);
}
