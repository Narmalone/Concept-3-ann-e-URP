using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UiManagerSession : MonoBehaviour
{
    #region delegates
    public static UiManagerSession instance { get; private set; }

    public delegate void ActiveActionDelegate();

    public ActiveActionDelegate CombatUi;
    #endregion

    [SerializeField] private UIDocument m_uiDocCombat;
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
        Debug.Log("activer l'interface");
    }
}
