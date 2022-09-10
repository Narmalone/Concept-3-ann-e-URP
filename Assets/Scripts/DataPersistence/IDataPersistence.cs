using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    //Seulement lire les donn�es
    void LoadData(GameData data);

    //ref car on veut autoriser le script � modifier les donn�es
    void SaveData(GameData data);
}
