using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager instance { get; private set; }

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
    public void AttackCharacter()
    {
        CastToPlayer(ChoiceSpellAgainstPlayer());

        //Démarrer coroutine pour faire réaparaître l'interface après un délai
        CombatManager.instance.StartCorou();
    }
    public Spell ChoiceSpellAgainstPlayer()
    { 
        //Besoin de rework avec l'ID de group car là ça prend forcément le premier mais si joueur pas contre le premier groupe ?
        //si il n'y a plus d'ennemis, alors on lance la fonction du combat manager qui met fin au combat -> vérif avec le count peut etre
        m_spellAgainstPlayer = GroupManager.instance.m_group1[Random.Range(0, GroupManager.instance.m_group1.Count)].m_thisCharacter.CurrentCharaSpell;
        Debug.Log(m_spellAgainstPlayer.Name);
        return m_spellAgainstPlayer;
    }

    //Classe qui comme ennemies / Player Attack servira pour lancer une attaque aux personnages du joueur

    //Cast depuis Combat Manager
    public void CastToPlayer(Spell spell)
    {
        CharactersOfPlayers selectedTarget = charaOfPlayers[Random.Range(0, playerCharacter.Count)];
        selectedTarget.GetDamage(spell);
    }

    public void CheckMobGroup()
    {
        if(GroupManager.instance.m_group1.Count == 0)
        {
            CombatManager.instance.delCombat(false);
        }
    }

}
