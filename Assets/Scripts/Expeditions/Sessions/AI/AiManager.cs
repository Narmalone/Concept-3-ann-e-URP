using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager instance { get; private set; }

    private List<Character> playerCharacter;
    private List<CharactersOfPlayers> m_ourTeam;
    public List<Ennemy> ennemyGroup;
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
    public void SetEnnemies()
    {
        m_ourTeam = CombatManager.instance.m_ourTeam;
        ennemyGroup = CombatManager.instance.m_enemyList;

        AttackCharacter();
    }
    public void AttackCharacter()
    {
        StartCoroutine(CorouBeforeAttack(2f));
        //Démarrer coroutine pour faire réaparaître l'interface après un délai
        CombatManager.instance.StartCorou();
    }
    public Spell ChoiceSpellAgainstPlayer()
    {
        m_spellAgainstPlayer = ennemyGroup[Random.Range(0, ennemyGroup.Count)].m_thisCharacter.CurrentCharaSpell;
        return m_spellAgainstPlayer;
    }

    //Classe qui comme ennemies / Player Attack servira pour lancer une attaque aux personnages du joueur

    //Cast depuis Combat Manager
    //rework le cast spell de l'iA et faire plutôt avec les checks du spell
    public void CastToPlayer(Spell spell)
    {

        if (spell.GeneralId != 1)
        {
            if(spell.m_isSolorOrMultipleTarget && spell.IsEnemyTarget == false)
            {
                foreach (CharactersOfPlayers chara in charaOfPlayers)
                {
                    chara.SpellCasted(spell);
                }
                Debug.Log("multiple & cible ennemy");
            }
            if(spell.m_isSolorOrMultipleTarget == false && spell.IsEnemyTarget == false)
            {
                CharactersOfPlayers selectedTarget = charaOfPlayers[Random.Range(0, playerCharacter.Count)];
                selectedTarget.SpellCasted(spell);
                Debug.Log("solo & cible ennemy");
            }
        }
        else
        {
            ennemyGroup = CombatManager.instance.m_enemyList;
            foreach (Ennemy mob in ennemyGroup)
            {
                mob.SpellCasted(spell);
            }
        }
    }

    public IEnumerator CorouBeforeAttack(float time)
    {
        yield return new WaitForSeconds(time);
        CastToPlayer(ChoiceSpellAgainstPlayer());
    }
}
