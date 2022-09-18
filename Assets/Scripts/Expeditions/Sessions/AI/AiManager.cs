using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager instance { get; private set; }

    [SerializeField] public List<Ennemies> mobGroup1;

    private Spells m_spellAgainstPlayer;

    private void Awake()
    {
        instance = this;
    }

    public void ChoiceSpellAgainstPlayer()
    {
        m_spellAgainstPlayer = mobGroup1[Random.Range(0, mobGroup1.Count)].m_enemySpell;
        Debug.Log(m_spellAgainstPlayer);
    }

    //Classe qui comme ennemies / Player Attack servira pour lancer une attaque aux personnages du joueur
    public void CastToPlayer()
    {
        //Une fois casté le joueur perd de la vie et c'est au tour du joueur donc CombatManager.instance.isTurnPlayer(true)
    }

}
