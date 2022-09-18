using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerTeam : MonoBehaviour, IDataPersistence
{
    public static LoadPlayerTeam instance { get; private set; }
    [System.NonSerialized] public List<Character> m_charactersTeam;
    [SerializeField] private GameObject prefabToInstatiate;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }
    public void LoadData(GameData data)
    {
        this.m_charactersTeam = data.m_playerCharactersOwnedData; 
    }

    public void SaveData(GameData data)
    {
        data.m_playerCharactersOwnedData = this.m_charactersTeam;
    }

}
