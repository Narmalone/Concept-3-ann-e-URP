using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    //Seulement lire les données
    void LoadData(GameData data);

    //ref car on veut autoriser le script à modifier les données
    void SaveData(GameData data);
}
