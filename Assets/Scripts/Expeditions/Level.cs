using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour, IDataPersistence
{
    [Header("References")]
    [SerializeField] private UIDocument m_uiDocument;
    [SerializeField] private LevelExpeditionsCreator m_lec;
    [Header("Propriétés")]
    [SerializeField] public string m_levelName;
    [SerializeField, Tooltip("Scene à charger lorsque le joueur appuis sur le bouton")] public string m_SceneToLoad;
    public int m_levelId = 0;
    public bool isLevelUnlocked = false;

    public Label m_levelDescription;
    public Button m_thisButton;
    [HideInInspector] public int m_playersProgressionsCount;

    //TO DO: Booléen qui nous sert à savoir si le niveau est bloqué faire un int dans la data pour savoir ou on en est dans la progression
    //Si bouton bloqué alors il n'est pas focusable
    //Sinon focusable
    //Trouver un moyen de fournir informations dans la database
    //peut etre pas besoin de mettre infos de la game dans data base mais plutôt scriptable object
    private void Start()
    {
        var rootElement = m_uiDocument.rootVisualElement;
        m_thisButton = m_lec.m_startExpeditionButton;

        if (isLevelUnlocked)
        {
            m_thisButton.SetEnabled(true);
        }
        else
        {
            m_thisButton.SetEnabled(false);
        }
    }
    
    public void OnCliquedExpedition()
    {
        SceneManager.LoadSceneAsync(m_SceneToLoad);
        Debug.Log("Le joueur a cliqué lancé le niveau: " + m_levelName + m_levelId);
    }
    public void LoadData(GameData data)
    {
        data.m_currentPlayersProgression = this.m_playersProgressionsCount;
    }

    public void SaveData(GameData data)
    {
        this.m_playersProgressionsCount = data.m_currentPlayersProgression;
    }
}
