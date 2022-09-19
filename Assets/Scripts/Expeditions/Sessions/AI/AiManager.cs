using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager instance { get; private set; }

    [SerializeField] public List<Ennemies> mobGroup1;
    private List<Character> playerCharacter;
    private Character selectedTarget;
    private Spells m_spellAgainstPlayer;
    [SerializeField] private List<CharactersOfPlayers> charaOfPlayers;
    [HideInInspector] public int index;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerCharacter = LoadPlayerTeam.instance.m_charactersTeam;
    }
    public void ChoiceSpellAgainstPlayer()
    {
        //si il n'y a plus d'ennemis, alors on lance la fonction du combat manager qui met fin au combat -> vérif avec le count peut etre
        m_spellAgainstPlayer = mobGroup1[Random.Range(0, mobGroup1.Count)].m_enemySpell;
        CastToPlayer();
    }

    //Classe qui comme ennemies / Player Attack servira pour lancer une attaque aux personnages du joueur
    public void CastToPlayer()
    {
        selectedTarget = playerCharacter[Random.Range(0, playerCharacter.Count)];

        //si index marche alors index = à l'ID du truc et on lance la fonction getdamage ou on vérifie si index == indexof//
        foreach(CharactersOfPlayers chara in charaOfPlayers)
        {
            index = playerCharacter.IndexOf(selectedTarget);
            chara.GetDamage(m_spellAgainstPlayer);
            Debug.Log(index);
        }
    }

}
