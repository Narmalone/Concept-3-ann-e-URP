using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class GoldText : MonoBehaviour, IDataPersistence
{

    #region variables

    [Header("Ui Document")]
    [SerializeField] private UIDocument m_Document;
    //Objectifs d'or du joueur
    [Header("Objectifs à atteindre d'or du joueur")]
    [SerializeField] private float[] m_playerGoldObjectives;
    private int m_playerGoldObjectivesCount;

    //Si le joueur est rang max
    private bool m_isMaxReached = false;

    private Label m_playerMoneyLabel;
    private Button m_freeGoldButton;

    //Indicateur d'or du joueur
    private float m_playerGoldCount = 0;

    //référence de données au titre du joueur
    [SerializeField, Tooltip("Référence pour avoir les titres du joueur")] private PlayersTitle m_playersTitle;

    #endregion

    #region Interface data Persistence
    public void LoadData(GameData data)
    {
        m_playerGoldCount = data.m_playerMoney;
        m_isMaxReached = data.isMaxPlayerTitleReached;
    }

    public void SaveData(GameData data)
    {
        data.m_playerMoney = this.m_playerGoldCount;
        data.isMaxPlayerTitleReached = this.m_isMaxReached;
    }
    #endregion

    //Fonction de debug pour tester la montée en or du joueur, les objectifs et l'update de ces dernier//
    public void FreeGoldClicked()
    {
        m_playerGoldCount += 50;
        UpdateGold();
        Debug.Log(Application.persistentDataPath);
    }
    private void Awake()
    {
        var rootElement = m_Document.rootVisualElement;

        m_playerMoneyLabel = rootElement.Q<Label>("PlayersMoneyLabel");
        m_freeGoldButton = rootElement.Q<Button>("GiveFreeGolds");
        m_freeGoldButton.clickable.clicked += FreeGoldClicked;
    }
    private void Start()
    {
        UpdateGold();
    }

    //Pour update le texte et lancer la fonction pour vérifier si le joueur à atteint un objectif ou non
    private void UpdateGold()
    {
        if (m_isMaxReached == false)
        {
            CheckGoldObjectives();
        }
        m_playerMoneyLabel.text =  "Gold: " + m_playerGoldCount.ToString();
        Debug.Log(m_playerGoldCount);
    }

    //Vérifie si le joueur a remplis un objectif, si oui passe au titre suivant, si le joueur est rang max
    //On ne vérifie plus
    private void CheckGoldObjectives()
    {
        if (m_playerGoldObjectivesCount == m_playerGoldObjectives.Length - 1)
        {
            m_isMaxReached = true;
        }
        else
        {
            if (m_isMaxReached == false)
            {
                if (m_playerGoldCount >= m_playerGoldObjectives[m_playerGoldObjectivesCount])
                {
                    m_playerGoldObjectivesCount++;
                    m_playersTitle.UpdatePlayersTitle();
                }
            }
            return;
        }
    }
}
