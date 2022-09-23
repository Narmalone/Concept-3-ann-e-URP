using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager instance { get; private set; }

    [SerializeField] public List<Ennemies> mobGroup1;
    private List<Character> playerCharacter;
    private Character selectedTarget;
    private Spell m_spellAgainstPlayer;
    [SerializeField] private List<CharactersOfPlayers> charaOfPlayers;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerCharacter = LoadPlayerTeam.instance.m_charactersTeam;
    }
    public void MerFunction()
    {
        CastToPlayer(ChoiceSpellAgainstPlayer());

        //D�marrer coroutine pour faire r�apara�tre l'interface apr�s un d�lai
        CombatManager.instance.StartCorou();
    }
    public Spell ChoiceSpellAgainstPlayer()
    {
        //si il n'y a plus d'ennemis, alors on lance la fonction du combat manager qui met fin au combat -> v�rif avec le count peut etre
        m_spellAgainstPlayer = mobGroup1[Random.Range(0, mobGroup1.Count)].m_enemySpell;

        return m_spellAgainstPlayer;
    }

    //Classe qui comme ennemies / Player Attack servira pour lancer une attaque aux personnages du joueur

    //Cast depuis ISpell
    public void CastToPlayer(Spell spell)
    {
        CharactersOfPlayers selectedTarget = charaOfPlayers[Random.Range(0, playerCharacter.Count)];

        selectedTarget.GetDamage(spell);
    }

    public void CheckMobGroup()
    {
        if(mobGroup1.Count == 0)
        {
            CombatManager.instance.delCombat(false);
        }
    }

}
