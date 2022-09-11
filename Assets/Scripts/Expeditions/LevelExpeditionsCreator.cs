using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class LevelExpeditionsCreator : MonoBehaviour
{
    [Header("Ui Document")]
    [SerializeField] private UIDocument m_UiDocument;

    private GroupBox m_expeditionSlot;
    private Button m_startExpeditionButton;

    private Label m_levelName;
    public Level[] m_levels;
    
    [SerializeField] VisualTreeAsset m_expeditionSlotTemplate;

    //génerer et instantier les slots d'expeditions
    public void CreateLevelUi()
    {
        //Créer les slots en fonction du nombre de niveau
        //Optimisable en ne mettant que ce qu'on veut que cela change
        foreach(Level level in m_levels)
        {
            if (level.isLevelUnlocked)
            {

                //Création des slots
                var rootElement = m_UiDocument.rootVisualElement;
                m_expeditionSlot = rootElement.Q<GroupBox>("ExpeditionSlot");

                TemplateContainer levelUi = m_expeditionSlotTemplate.Instantiate();
                rootElement.Add(levelUi);

                //Attribution du nom et level
                m_levelName = rootElement.Q<Label>("LevelName");
                m_levelName.text = level.m_levelName;
            }
            else
            {
                //Création des slots
                var rootElement = m_UiDocument.rootVisualElement;
                m_expeditionSlot = rootElement.Q<GroupBox>("ExpeditionSlot");

                TemplateContainer levelUi = m_expeditionSlotTemplate.Instantiate();
                rootElement.Add(levelUi);

                //désactive le slot si le joueur ne l'a pas débloqué
                levelUi.SetEnabled(false);

                //Attribution du nom du level
                m_levelName = rootElement.Q<Label>("LevelName");
                m_levelName.text = level.m_levelName;
            }
        }  
    }

    public void OncliquedExpedition()
    {

    }
}
