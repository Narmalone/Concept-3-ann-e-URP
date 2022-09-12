using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class LevelExpeditionsCreator : MonoBehaviour
{
    [Header("Ui Document")]
    [SerializeField] public UIDocument m_UiDocument;

    private GroupBox m_expeditionSlot;
    private Button m_startExpeditionButton;

    private Label m_levelName;
    public Level m_levels;
    
    [SerializeField] VisualTreeAsset m_expeditionSlotTemplate;

    private void Awake()
    {
        CreateLevelUi();
    }

    //génerer et instantier les slots d'expeditions
    public void CreateLevelUi()
    {
        //Créer les slots en fonction du nombre de niveau
        //Optimisable en ne mettant que ce qu'on veut que cela change
        //Création des slots

        var rootElement = m_UiDocument.rootVisualElement;
        //Instantier et ajouter dans la hiérarchie les templates de niveaux
        Debug.Log(rootElement.childCount);

        TemplateContainer levelUi = m_expeditionSlotTemplate.Instantiate();

        rootElement.Add(levelUi);

        //Attribution du nom et level
        m_levelName = rootElement.Q<Label>("LevelName");
        m_levelName.text = m_levels.m_levelName + m_levels.m_levelId;

        //Changer le nom du visual element pour pouvoir le récupérer dans la hiérarchie
        m_levelName.name = "Level: " + m_levels.m_levelId.ToString();

        if (m_levels.m_levelId == 0)
        {
            m_startExpeditionButton = rootElement.Q<Button>("StartExpedition");
            if (m_startExpeditionButton != null)
            {
                Debug.Log(m_startExpeditionButton.parent.name);
            }
            else
            {
                Debug.Log("ntm il est pas là");
            }
        }

        //foreach (Level level in m_levels)
        //{
           
        //}
       
    }
}
