using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerTeam : MonoBehaviour, IDataPersistence
{
    public static LoadPlayerTeam instance { get; private set; }
    public List<Character> m_charactersTeam;

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
        this.m_charactersTeam = data.m_playerTeam;
    }

    public void SaveData(GameData data)
    {
        data.m_playerTeam = this.m_charactersTeam;
    }

}
