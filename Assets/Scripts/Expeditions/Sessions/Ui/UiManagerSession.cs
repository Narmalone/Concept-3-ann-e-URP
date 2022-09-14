using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UiManagerSession : MonoBehaviour, IDataPersistence
{
    #region delegates
    public static UiManagerSession instance { get; private set; }

    public delegate void ActiveActionDelegate();

    public ActiveActionDelegate CombatUi;
    #endregion

    [SerializeField] private UIDocument m_uiDocCombat;
    private List<Character> m_characters;

    //Noms des sorts
    private Label SpellName1;
    private Label SpellName2;
    private Label SpellName3;
    private Label SpellName4;

    //dégâts des sorts
    private Label SpellDamage1;
    private Label SpellDamage2;

    //Boutons des sorts
    private Button Spell1;
    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;

        CombatUi = UiCombat;
        SetDisabledUi();
    }

    public void SetDisabledUi()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.None;
    }
    public void UiCombat()
    {
        var rootElement = m_uiDocCombat.rootVisualElement;
        rootElement.style.display = DisplayStyle.Flex;

        SpellName1 = rootElement.Q<Label>("SpellName1");
        SpellName1.text = m_characters[0].CurrentCharaSpell.SpellName;
       
        SpellDamage1 = rootElement.Q<Label>("SpellDamage1");
        SpellDamage1.text = m_characters[0].CurrentCharaSpell.SpellBasicDamage.ToString();

        Spell1 = rootElement.Q<Button>("BSpell1");
        //To DO: Quand le spell est lancé -> joueur doit cliquer sur un ennemie et lui faire des dégâts
        //To do: Désactiver le visual element pendant X tour
        Spell1.clickable.clicked += CombatManager.instance.SelectEnemies;

    }

    public void LoadData(GameData data)
    {
        m_characters = data.m_playerCharactersOwnedData;
    }

    public void SaveData(GameData data)
    {
        return;
    }
}
