using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters : MonoBehaviour
{
    private PlayersCharacter m_characters;

    private void Awake()
    {
        m_characters = new PlayersCharacter();

        m_characters.CharactersName = "Simone de Beauvoir";
        m_characters.CharactersLife = 40;
        m_characters.CharactersRarity = 1;
        m_characters.CharactersDamage = 5;
        m_characters.CharactersDefense = 5;
    }
    private void Start()
    {
        Debug.Log(m_characters.CharactersDamage);
    }
}
