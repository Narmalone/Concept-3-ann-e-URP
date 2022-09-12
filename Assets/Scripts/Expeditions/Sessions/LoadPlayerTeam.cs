using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerTeam : MonoBehaviour, IDataPersistence
{

    private List<Character> m_charactersTeam;
    [SerializeField] private GameObject prefabToInstatiate;

    private void Start()
    {
        foreach(Character chara in m_charactersTeam)
        {
            Instantiate(prefabToInstatiate, new Vector3(0, 0, 0), Quaternion.identity);
            Debug.Log("Doit avoir été crée");
        }
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
