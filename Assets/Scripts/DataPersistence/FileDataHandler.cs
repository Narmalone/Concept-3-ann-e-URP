using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{

    //Pour définir le chamin des données
    private string dataDirPath = "";

    private string dataFileName = "";

    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "narmalone";
    private readonly string backupExtension = ".bak";
    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.useEncryption = useEncryption;
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    //Profile id est pour la génération de directory pour faire de multiples slots de sauvegardes
    public GameData Load(string profileId, bool allowRestoreFromBackup = true)
    {

        //si le profil ID est null return pour se tailler
        if(profileId == null)
        {
            return null;
        }

        //Path.Combine est une méthode qui est destinée à concaténer des chaînes individuelles en une seule chaîne qui représente un chemin d’accès au fichier.
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //charger les données sérialisées depuis le fichier
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }

                    //Si data utilisée
                    if (useEncryption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }


                    //Désérialiser les données depuis le JSON
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);


                }
            }
            catch(Exception e)
            {

                //Depuis que l'on appelle récursivement Load(), les rollbacks réussissent mais les données
                //n'arrivent tjr pas à se charger pour d'autres raisons
                //qui ne peut être check à cause d'une loop infinie (récursivité)
                if (allowRestoreFromBackup)
                {
                    Debug.LogWarning("Erreur en essayant de charger les data depuis le fichier" + fullPath + "\n" + e);
                    bool rollbackSucess = AttemptRollback(fullPath);
                    if (rollbackSucess)
                    {
                        //essai de charger à nouveau les données de manière récursive
                        loadedData = Load(profileId, false);
                    }
                }
                else
                {
                    Debug.LogError("Une erreur s'est produite en essayer de charger le fichier à: " + fullPath + "et le backup ne marche pas." + "\n" + e);
                }
            }
        }
        return loadedData;
    }

    public void Save(GameData data, string profileId)
    {

        //si le profil ID est null return pour se tailler
        if (profileId == null)
        {
            return;
        }

        //Path.Combine est une méthode qui est destinée à concaténer des chaînes individuelles en une seule chaîne qui représente un chemin d’accès au fichier.
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        string backupFilePath = fullPath + backupExtension;
        try
        {
            //Créer le fichier directeur qui doit être écrit si il n'existe pas déjà
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialiser les game data object dans un Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //Autoriser à encrypter la data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //écrire les données sérialisées sur un fichier
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            //vérifier le nouveau fichier qui peut-être chargé avec succès
            GameData verifiedGameData = Load(profileId);

            //si les données peuvent-être vérifiés alors activer backup
            if(verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            else
            {
                throw new Exception("Le fichier sauvegardé ne peut pas être vérifié et le backup ne peut pas être créer");
            }

        }
        catch(Exception e)
        {
            Debug.LogError("Error produite lors de la sauvegarde des données vers le fichier" + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionnary = new Dictionary<string, GameData>();

        //Loop tous les noms des répertoires dans les chemins des données
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach(DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            //Checker si le fichier de donnée existe, si non alors ce dossier n'est pas un profile et doit être ignoré

            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Le fichier suivants ne contient pas de données: " + profileId);
                continue;
            }

            //Charger les données du jeu pour se profile et le mettre dans le dictionnaire
            GameData profileData = Load(profileId);

            //Check si les données du profil ne sont pas nulles parce que si ça l'es, un message d'erreur devrait apparaître
            if(profileData != null)
            {
                profileDictionnary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("A essayé de chargé le profil mais quelque chose ne va pas. ProfileId: " + profileId);
            }

        }


        return profileDictionnary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();

        //La key est le profile Id
        foreach(KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            //Passer cette entrée si les données sont nulles
            if(gameData == null)
            {
                continue;
            }

            //Si c'est la première donnée que l'on a chargé c'est la plus récente
            if(mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            //Sinon comparer pour voir si quelle date est la plus récente
            else
            {
                DateTime mostRecentDataTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].m_lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.m_lastUpdated);

                //Le plus grand temps est le plus récent
                if(newDateTime > mostRecentDataTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }

    //création d'encryptage XOR
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }

    private bool AttemptRollback(string fullPath)
    {
        bool sucess = false;
        string backupFilePath = fullPath + backupExtension;
        try
        {
            //SI le fichier existe, On essaie de overwrite l'original
            if (File.Exists(backupFilePath))
            {
                File.Copy(backupFilePath, fullPath, true);
                sucess = true;
                Debug.LogWarning("Rollback du fichier de données à: " + backupFilePath);
            }
            else
            {
                throw new Exception("Le fichier a essayé de rollback mais il n'existe pas");
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Une erreur a été trouvée en essayer d'utiliser le backup file à: " + backupFilePath + "\n" + e);
        }

        return sucess;
    }
}
