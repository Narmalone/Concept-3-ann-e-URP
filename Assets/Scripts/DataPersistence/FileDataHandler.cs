using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{

    //Pour d�finir le chamin des donn�es
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

    //Profile id est pour la g�n�ration de directory pour faire de multiples slots de sauvegardes
    public GameData Load(string profileId, bool allowRestoreFromBackup = true)
    {

        //si le profil ID est null return pour se tailler
        if(profileId == null)
        {
            return null;
        }

        //Path.Combine est une m�thode qui est destin�e � concat�ner des cha�nes individuelles en une seule cha�ne qui repr�sente un chemin d�acc�s au fichier.
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //charger les donn�es s�rialis�es depuis le fichier
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }

                    //Si data utilis�e
                    if (useEncryption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }


                    //D�s�rialiser les donn�es depuis le JSON
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);


                }
            }
            catch(Exception e)
            {

                //Depuis que l'on appelle r�cursivement Load(), les rollbacks r�ussissent mais les donn�es
                //n'arrivent tjr pas � se charger pour d'autres raisons
                //qui ne peut �tre check � cause d'une loop infinie (r�cursivit�)
                if (allowRestoreFromBackup)
                {
                    Debug.LogWarning("Erreur en essayant de charger les data depuis le fichier" + fullPath + "\n" + e);
                    bool rollbackSucess = AttemptRollback(fullPath);
                    if (rollbackSucess)
                    {
                        //essai de charger � nouveau les donn�es de mani�re r�cursive
                        loadedData = Load(profileId, false);
                    }
                }
                else
                {
                    Debug.LogError("Une erreur s'est produite en essayer de charger le fichier �: " + fullPath + "et le backup ne marche pas." + "\n" + e);
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

        //Path.Combine est une m�thode qui est destin�e � concat�ner des cha�nes individuelles en une seule cha�ne qui repr�sente un chemin d�acc�s au fichier.
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        string backupFilePath = fullPath + backupExtension;
        try
        {
            //Cr�er le fichier directeur qui doit �tre �crit si il n'existe pas d�j�
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialiser les game data object dans un Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //Autoriser � encrypter la data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //�crire les donn�es s�rialis�es sur un fichier
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            //v�rifier le nouveau fichier qui peut-�tre charg� avec succ�s
            GameData verifiedGameData = Load(profileId);

            //si les donn�es peuvent-�tre v�rifi�s alors activer backup
            if(verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            else
            {
                throw new Exception("Le fichier sauvegard� ne peut pas �tre v�rifi� et le backup ne peut pas �tre cr�er");
            }

        }
        catch(Exception e)
        {
            Debug.LogError("Error produite lors de la sauvegarde des donn�es vers le fichier" + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionnary = new Dictionary<string, GameData>();

        //Loop tous les noms des r�pertoires dans les chemins des donn�es
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach(DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            //Checker si le fichier de donn�e existe, si non alors ce dossier n'est pas un profile et doit �tre ignor�

            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Le fichier suivants ne contient pas de donn�es: " + profileId);
                continue;
            }

            //Charger les donn�es du jeu pour se profile et le mettre dans le dictionnaire
            GameData profileData = Load(profileId);

            //Check si les donn�es du profil ne sont pas nulles parce que si �a l'es, un message d'erreur devrait appara�tre
            if(profileData != null)
            {
                profileDictionnary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("A essay� de charg� le profil mais quelque chose ne va pas. ProfileId: " + profileId);
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

            //Passer cette entr�e si les donn�es sont nulles
            if(gameData == null)
            {
                continue;
            }

            //Si c'est la premi�re donn�e que l'on a charg� c'est la plus r�cente
            if(mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            //Sinon comparer pour voir si quelle date est la plus r�cente
            else
            {
                DateTime mostRecentDataTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].m_lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.m_lastUpdated);

                //Le plus grand temps est le plus r�cent
                if(newDateTime > mostRecentDataTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }

    //cr�ation d'encryptage XOR
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
                Debug.LogWarning("Rollback du fichier de donn�es �: " + backupFilePath);
            }
            else
            {
                throw new Exception("Le fichier a essay� de rollback mais il n'existe pas");
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Une erreur a �t� trouv�e en essayer d'utiliser le backup file �: " + backupFilePath + "\n" + e);
        }

        return sucess;
    }
}
