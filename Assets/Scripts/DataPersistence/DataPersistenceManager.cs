using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    //Debugging dans la scene du jeu -> City ou Expeditions
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIsNull = false;

    [SerializeField] private bool disableDataPersistence = false;

    [SerializeField] private bool overrideSelectedProfileId = false;

    [SerializeField] private string testSelectedProfileId = "test";

    [Header("Fichier de stockage config")]
    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistencesObjects;

    private FileDataHandler dataHandler;

    [SerializeField] private bool useEncryption = false;

    //Singleton le manager
    public static DataPersistenceManager instance { get; private set; }


    //noms des dossiers
    private string selectedProfileId = "";

    [Header("Configuration de sauvegarde automatique")]

    //d�fauts = 5 min
    [SerializeField] private float m_sauvegardeAutoDelay = 300f;
    private Coroutine m_coroutineSauvegarde;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        //Cr�ation du singleton
        if(instance != null)
        {
            //Debug.LogError("Plus d'une data persistence manager dans la scene, destruction du nouveau manager");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence est actuellement d�sactiv�");
        }

        //Application.persistant datapath donne le r�pertoire standard tu syst exploitation pour la persistance des donn�es dans le projet unity
        //https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html//
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        Debug.Log(Application.persistentDataPath);
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();

        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Override le nom du profil id s�lectionn� par le test id: " + testSelectedProfileId);
        }
    }

    #region Game Functions

    //Cr�ation de la nouvelle partie
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public void LoadGame()
    {

        //Return si data persistence est d�sactiv�
        if (disableDataPersistence)
        {
            return;
        }

        //Charger n'importe quelle donn�es sauvegard� ou celle cliqu�
        //si pas de data, cr�er une game
        this.gameData = dataHandler.Load(selectedProfileId);


        //Commencer une nouvelle partie si les donn�es sont nulles et les donn�es sont configur�es pour du d�boggage
        if(this.gameData == null && initializeDataIsNull)
        {
            NewGame();
        }

        //Si pas de donn�e, on ne peut pas lancer, NewGame() � la place du return si on veut que m�me
        //si aucune partie n'a �t� faite quand on continue, cela puisse la cr�er
        if(this.gameData == null)
        {
            Debug.Log("Pas de donn�es trouv�es. Un nouvelle partie doit �tre commenc�e avant que les donn�es ne soit charg�e.");
            return;
        }

        //Pousser les donn�es charg�es dans tous les autres scripts qui en ont besoin

        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {

        //Return si data persistence est d�sactiv�
        if (disableDataPersistence)
        {
            return;
        }

        //Si il n'y a pas de donn�es � sauvegarder, log warning
        if (this.gameData == null)
        {
            Debug.LogWarning("Pas de donn�es trouv�es. Une nouvelle partie doit �tre commenc�e avant que les donn�es ne soit sauvegard�es");
            return;
        }

        //passer les donn�es dans les autres scripts pour qu'ils puissent l'update
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        //Timestamp les donn�es pour savoir la derni�re sauvegarde
        //Get le date et temps actuelle pour le syst�me qui est actif et on les s�rializes en binaire qui
        //peut �tre sauvegard� plus facilement
        gameData.m_lastUpdated = System.DateTime.Now.ToBinary();

        //sauvegarder les donn�es dans un fichier en utilisant le data handler
        dataHandler.Save(gameData, selectedProfileId);
    }


    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
    #endregion

    //Fonction qui permet de retrouver tous les monobehaviour qui utilisent l'interface Data Persistence
    private List<IDataPersistence> FindAllDataPersistencesObjects()
    {

        //FindObjectsofType prend dans un bool�en optionnel pour inclure les objets inactifs
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    #region Scenes functions
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistencesObjects = FindAllDataPersistencesObjects();
        LoadGame();

        //Lancer la coroutine de sauvegarde automatique
        if(m_coroutineSauvegarde != null)
        {
            StopCoroutine(AutoSave());
        }
        m_coroutineSauvegarde = StartCoroutine(AutoSave());

    }
    #endregion

    #region Application quit
    //quand l'application est quitt� -> sauvegarde le automatiquement la progression du joueur
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    #endregion

    public void ChangeSelectedProfileID(string newProfileId)
    {
        //update le profile pour utiliser sauvegarder et charger
        this.selectedProfileId = newProfileId;

        //charger la partie quui utilise ce profil, update les donn�es du jeux 
        LoadGame();
    }

    //Coroutine qui vas sauvegarder tous les X temps
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_sauvegardeAutoDelay);
            SaveGame();
            Debug.Log("La partie a �t� sauvegard�e automatiquement");
        }
    }
}
