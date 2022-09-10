using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SaveSlots : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;

    [SerializeField] private TextMeshProUGUI titlePlayer;

    [SerializeField] private TextMeshProUGUI playersMoney;

    private Button m_saveSlotButton;

    private void Awake()
    {
        m_saveSlotButton = this.GetComponent<Button>();
    }

    //Mettre les informations dans la save slot
    public void SetData(GameData data)
    {
        if(data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            titlePlayer.text = data.m_currentPlayersTitle;
            playersMoney.text = "Argent: " + data.m_playerMoney;

        }
    }

    //fonction pour récupérer cette slot
    public string GetProfileID()
    {
        return this.profileId;
    }

    public void SetInteractible(bool interactable)
    {
        m_saveSlotButton.interactable = interactable;
    }
}
