using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Level : MonoBehaviour, IDataPersistence
{
    [Header("OnGUI")]
    [SerializeField] private UIDocument m_UiDocument;
    [SerializeField] public string m_levelName;
    public Label m_levelDescription;
    public int m_levelId = 0;

    [SerializeField] LevelExpeditionsCreator m_LEC;

    private Button m_thisButton;
    public bool isLevelUnlocked = false;
    public int m_playersProgressionsCount;

    //TO DO: Booléen qui nous sert à savoir si le niveau est bloqué faire un int dans la data pour savoir ou on en est dans la progression
    //Si bouton bloqué alors il n'est pas focusable
    //Sinon focusable
    //Trouver un moyen de fournir informations dans la database
    //peut etre pas besoin de mettre infos de la game dans data base mais plutôt scriptable object

    private void Awake()
    {

        if (m_levelId <= m_playersProgressionsCount)
        {
            isLevelUnlocked = true;
            m_LEC.CreateLevelUi();
            Debug.Log("Le joueur a débloqué le niveau: " + m_levelId);
        }

    }
    public void OnCliquedExpedition()
    {
        //To DO: Lancer la mission on button cliqued + load scene with level ID;
        Debug.Log("Le joueur a cliqué sur un niveau débloqué: " + m_levelId);
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
