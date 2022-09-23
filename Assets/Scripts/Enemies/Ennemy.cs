using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField, Tooltip("SO auquel cet ennemi appartiens")] private Group m_thisEnemyGroup;
    [SerializeField, Tooltip("Id de l'ennemi correspondant � sa position dans la liste du group")] private int m_id;

    private Character m_thisCharacter;

    private void Start()
    {
        SetData();
    }

    public void SetData()
    {
        //Le personnage �quivaut � chercher dans son groupe la liste d'ennemies en fonction de son propre id
        m_thisCharacter = m_thisEnemyGroup.EnnemiesList[m_id];

        GetComponentInChildren<LifeBarHandler>().SetMaxHealth(m_thisCharacter.CharactersLife);
    }

    public Spell GetDamage(Spell target)
    {
        return target;
    }
}
