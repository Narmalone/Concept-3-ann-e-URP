using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerTeam : MonoBehaviour, IDataPersistence
{

    private List<Character> m_charactersTeam;
    [SerializeField] private GameObject prefabToInstatiate;

    private void Awake()
    {
        //StartCoroutine(CoroutineInstantiate());
        Debug.Log(m_charactersTeam);
       
    }
    private void Start()
    {
        foreach (Character chara in m_charactersTeam)
        {
            Instantiate(prefabToInstatiate, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }
    public IEnumerator CoroutineInstantiate()
    {
        foreach (Character chara in m_charactersTeam)
        {
            Instantiate(prefabToInstatiate, new Vector3(0f, 0f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(2);
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
