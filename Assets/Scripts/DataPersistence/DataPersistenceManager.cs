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

    //défauts = 5 min
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
        //Création du singleton
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
            Debug.LogWarning("Data Persistence est actuellement désactivé");
        }

        //Application.persistant datapath donne le répertoire standard tu syst exploitation pour la persistance des données dans le projet unity
        //https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html//
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        Debug.Log(Application.persistentDataPath);
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();

        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Override le nom du profil id sélectionné par le test id: " + testSelectedProfileId);
        }
    }

    #region Game Functions

    //Création de la nouvelle partie
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public void LoadGame()
    {

        //Return si data persistence est désactivé
        if (disableDataPersistence)
        {
            return;
        }

        //Charger n'importe quelle données sauvegardé ou celle cliqué
        //si pas de data, créer une game
        this.gameData = dataHandler.Load(selectedProfileId);


        //Commencer une nouvelle partie si les données sont nulles et les données sont configurées pour du déboggage
        if(this.gameData == null && initializeDataIsNull)
        {
            NewGame();
        }

        //Si pas de donnée, on ne peut pas lancer, NewGame() à la place du return si on veut que même
        //si aucune partie n'a été faite quand on continue, cela puisse la créer
        if(this.gameData == null)
        {
            Debug.Log("Pas de données trouvées. Un nouvelle partie doit être commencée avant que les données ne soit chargée.");
            return;
        }

        //Pousser les données chargées dans tous les autres scripts qui en ont besoin

        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {

        //Return si data persistence est désactivé
        if (disableDataPersistence)
        {
            return;
        }

        //Si il n'y a pas de données à sauvegarder, log warning
        if (this.gameData == null)
        {
            Debug.LogWarning("Pas de données trouvées. Une nouvelle partie doit être commencée avant que les données ne soit sauvegardées");
            return;
        }

        //passer les données dans les autres scripts pour qu'ils puissent l'update
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        //Timestamp les données pour savoir la dernière sauvegarde
        //Get le date et temps actuelle pour le système qui est actif et on les sérializes en binaire qui
        //peut être sauvegardé plus facilement
        gameData.m_lastUpdated = System.DateTime.Now.ToBinary();

        //sauvegarder les données dans un fichier en utilisant le data handler
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

        //FindObjectsofType prend dans un booléen optionnel pour inclure les objets inactifs
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
    //quand l'application est quitté -> sauvegarde le automatiquement la progression du joueur
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    #endregion

    public void ChangeSelectedProfileID(string newProfileId)
    {
        //update le profile pour utiliser sauvegarder et charger
        this.selectedProfileId = newProfileId;

        //charger la partie quui utilise ce profil, update les données du jeux 
        LoadGame();
    }

    //Coroutine qui vas sauvegarder tous les X temps
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_sauvegardeAutoDelay);
            SaveGame();
        }
    }
}
