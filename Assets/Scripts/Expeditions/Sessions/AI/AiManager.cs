using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager instance { get; private set; }

    private List<Character> playerCharacter;
    private List<Ennemy> ennemyGroup;
    private Character selectedTarget;
    private Character ennemyTarget;
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
    public void AttackCharacter()
    {
        CastToPlayer(ChoiceSpellAgainstPlayer());

        //Démarrer coroutine pour faire réaparaître l'interface après un délai
        CombatManager.instance.StartCorou();
    }
    public Spell ChoiceSpellAgainstPlayer()
    { 
        //CHECK SI l'ID ne correspond pas à un sort de heal
        m_spellAgainstPlayer = GroupManager.instance.m_group1[Random.Range(0, GroupManager.instance.m_group1.Count)].m_thisCharacter.CurrentCharaSpell;
        Debug.Log(m_spellAgainstPlayer.Name);
        return m_spellAgainstPlayer;
    }

    //Classe qui comme ennemies / Player Attack servira pour lancer une attaque aux personnages du joueur

    //Cast depuis Combat Manager
    public void CastToPlayer(Spell spell)
    {
        if (spell.GeneralId != 1)
        {
            CharactersOfPlayers selectedTarget = charaOfPlayers[Random.Range(0, playerCharacter.Count)];
            selectedTarget.GetDamage(spell);
        }
        else
        {
            ennemyGroup = CombatManager.instance.m_enemyList;
            foreach(Ennemy mob in ennemyGroup)
            {
                mob.SpellCasted(spell);
            }
        }
    }

    public void CheckMobGroup()
    {
        if(GroupManager.instance.m_group1.Count == 0)
        {
            CombatManager.instance.delCombat(false);
        }
    }

}
