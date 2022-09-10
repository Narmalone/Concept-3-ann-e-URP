using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    [Header("Button")]
    [SerializeField, Tooltip("Référence au bouton servant à faire apparaître/disparaître l'interface de navigation")] private Button m_navigateButton;
    [SerializeField, Tooltip("Référence au bouton servant retourner au menu en sauvegardant")] private Button m_backButton;

    [Header("Check")]
    [SerializeField, Tooltip("Permet de savoir si le pannel est actif ou non")] private bool m_enablePannel = false;

    [Header("Animations")]
    [SerializeField, Tooltip("Permettre de trigger l'animation d'activer/désactiver le pannel par un animator")] private Animator m_EnablePannelAnimator;

    private void Awake()
    {
        //Initialiser sous variable locale le bouton de navigation//
        Button btn = m_navigateButton.GetComponent<Button>();
        Button backButton = m_backButton.GetComponent<Button>();

        //Créer une fonction quand on click sur le bouton
        btn.onClick.AddListener(TaskOnClick);
        backButton.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {

        //sauvegarder le jeux
        DataPersistenceManager.instance.SaveGame();

        //charger le Main Menu
        SceneManager.LoadSceneAsync("MainMenu");
    }


    //Fonction lorsque le bouton navigation est cliqué//
    private void TaskOnClick()
    {
        if(m_enablePannel == false)
        {
            m_enablePannel = true;
            m_EnablePannelAnimator.SetTrigger("OnEnable");
        }
        else 
        {
            m_enablePannel = false;
            m_EnablePannelAnimator.SetTrigger("OnDisable");
        }
    }

}
