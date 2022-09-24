using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CombatManager : MonoBehaviour
{
    #region delegates
    public static CombatManager instance { get; private set; }

    public delegate bool ActionDelegate(bool boolValue);

    public ActionDelegate OnStartCombat;
    public ActionDelegate delCombat;

    public ActionDelegate delTurn;
    #endregion

    public bool canSelectMob = false;
    public bool isFighting = false;

    public List<CharactersOfPlayers> m_ourTeam;
    public List<Character> m_ennemyTeam;

    public List<Ennemy> m_enemyList;
    public int GroupIdInFight;

    [HideInInspector] public Group m_currentFightingGroup;

    [SerializeField] private LayerMask m_enemyMask;
    [SerializeField] private LayerMask m_charactersMask;

    //Spell qui correspond au spell du boutton que le joueur a cliqu�
    private Spell m_currentSpellSelected;

    private void Awake()
    {
        //cr�ation mini-singleton
        if(instance != null)
        {
            return;
        }
        instance = this;

        //delegates du joueur combat 
        OnStartCombat = StartCombat;
        delCombat = InCombat;

        delTurn = IsPlayerTurn;
    }

    //Si le joueur est en combat
    private bool StartCombat(bool boolValue)
    {
        if (boolValue)
        {
            if(GroupIdInFight == 0)
            {
                m_enemyList = GroupManager.instance.m_group1;
            } 
            else if(GroupIdInFight == 1)
            {
                m_enemyList = GroupManager.instance.m_group2;
            }
            Debug.Log("Combat contre le groupe d'ennemis ID: " + GroupIdInFight + " a �t� lanc�");
            //Si combat commence alors c'est le tour du joueur
            delTurn(true);
            delCombat(true);
            UiManagerSession.instance.CombatUi();

            //Set the ennmy team from the group we are fighting against
            m_ennemyTeam = m_currentFightingGroup.EnnemiesList;
        }
        else
        {
            Debug.LogError("Quelque chose � lanc� un combat mais la value est � false ? " + boolValue);
        }
        return boolValue;
    }

    private bool InCombat(bool boolValue)
    {
        if (boolValue)
        {
            isFighting = true;
            FindObjectOfType<PlayerControllerCity>().enabled = false;
        }
        else
        {
            //Si joueur plus en combat on lui redonne le contr�le
            isFighting = false;
            FindObjectOfType<PlayerControllerCity>().enabled = true;

            //Reset player's Turn
            delTurn(false);
            UiManagerSession.instance.NotPlayerTurn();

            //Clear la liste d'ennemis et le groupe contre lequel on est
            m_currentFightingGroup = new Group();
        }
        return boolValue;
    }

    //V�rifier si c'est au tour du joueur
    //Sinon d�sactiver tous les boutons dans l'interfaces du joueur
    //Si le spell a d�j� �t� utilis� le spell resteras bloqu�
    private bool IsPlayerTurn(bool boolValue)
    {
        if (boolValue)
        {
            //Si tour du joueur
            //Interface joueur activ�e et l'IA ne joue pas
            UiManagerSession.instance.PlayerTurn();
        }
        else
        {
            //si ce n'est pas le temps du joueur
            //d�sactiver l'interface puis c'est au tour de l'IA
            UiManagerSession.instance.NotPlayerTurn();

            if (isFighting)
            {
                //Ai manager pour faire jouer l'IA
                AiManager.instance.AttackCharacter();
            }
        }
        return boolValue;
    }

    public void StartCorou()
    {
        StartCoroutine(CorouBeforeTurn());
    }

    public IEnumerator CorouBeforeTurn()
    {
        yield return new WaitForSeconds(2);

        if (isFighting)
        {
            //D�sactiver le bouton sort que le joueur � lanc�
            delTurn(true);
        }
       
    }

    //Fonction qui permmet de savoir qu'est-ce qui est une cible ou pas dans le sort avant de le cast
    public void CheckSpellTarget(Spell spellCliqued)
    {
        //Une fois que le check est fait dire quelles sont les target possibles en fonction des boolens
        //mettre un check dans les ennemies ? Genre si y'a que notre �quipe qui est targetable bah pas les ennemis ? -> avec un foreach ennemies in list
        //Si Un seul ennemi est targetable comment faire ? 
        m_currentSpellSelected = spellCliqued;
        //trouver comment avoir le bon groupe des ennemis
        if (m_currentSpellSelected.IsSoloTarget && m_currentSpellSelected.IsEnemyTarget)
        {
            //Multiples cibles et notre �quipe par exemple le sort de heal
            //Ce genre de sort on le Cast direct car cela touche toute les cibles

            //FAIRE LE CHECK DANS L'INTERFACE SPELL PLUTOT ??? mais dans ce cas comment comprendre quel spell en fonction du bouton
            Debug.Log("Plusieures cibles de notre �quipe");
            foreach (Ennemy ennemies in m_enemyList)
            {
                ennemies.isTargetable = false;
            }

            foreach (CharactersOfPlayers ourChara in m_ourTeam)
            {
                ourChara.isTargetable = true;
            }
        }
        if (m_currentSpellSelected.IsSoloTarget && m_currentSpellSelected.IsEnemyTarget == false)
        {
            // Si plusieures cibles et c'est l'equipe ennemie
            Debug.Log("Plusieurs ennemis sont castables");
            foreach (Ennemy ennemies in m_enemyList)
            {
                ennemies.isTargetable = true;
            }

            foreach(CharactersOfPlayers ourChara in m_ourTeam)
            {
                ourChara.isTargetable = false;
            }
        }
        if (m_currentSpellSelected.IsSoloTarget == false && m_currentSpellSelected.IsEnemyTarget)
        {
            //Si une seule cible et que c'est notre �quipe
            Debug.Log("Une seule cible de notre �quipe");
            foreach (Ennemy ennemies in m_enemyList)
            {
                ennemies.isTargetable = false;
            }

            foreach (CharactersOfPlayers ourChara in m_ourTeam)
            {
                ourChara.isTargetable = true;
            }
        }
        if(m_currentSpellSelected.IsSoloTarget == false && m_currentSpellSelected.IsEnemyTarget == false)
        {
            //Si une seule cible et que c'est celle de l'�quipe ennemie
            Debug.Log("Une seule cible ennemie");
            foreach (Ennemy ennemies in m_enemyList)
            {
                ennemies.isTargetable = true;
            }

            foreach (CharactersOfPlayers ourChara in m_ourTeam)
            {
                ourChara.isTargetable = false;
            }
        }
        //Debug.Log(m_currentSpellSelected.Name);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Si le joueur n'a rien s�lectionn� on ne fait rien
            if (m_currentSpellSelected == null) return;

            //raycast quand le joueur clique envoyer le sort � la target
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, m_enemyMask))
            {
                
                //Check si la cible est targetable ou pas r�fl�chir � un autre moyen pour v�rifier
                if(hit.collider.gameObject.GetComponent<Ennemy>().isTargetable == true)
                {
                    //si la cible est une target on cast
                    //CastSpell(hit.collider.gameObject.GetComponent<Ennemy>().m_thisCharacter, m_currentSpellSelected);
                    hit.collider.gameObject.GetComponent<Ennemy>().SpellCasted(m_currentSpellSelected);
                    UiManagerSession.instance.SpellUsed();
                    //Puis update la vie du perso
                    //hit.collider.gameObject.GetComponent<Ennemy>().UpdateLife();
                }

                //une fois que le joueur a lanc� le sort il n'est plus s�lectionn�
                m_currentSpellSelected = null;
            }
            
            else if (Physics.Raycast(ray, out hit, 1000, m_charactersMask))
            {
                
                //Check si la cible est targetable ou pas r�fl�chir � un autre moyen pour v�rifier
                if(hit.collider.gameObject.GetComponent<CharactersOfPlayers>().isTargetable == true)
                {
                    //si la cible est une target on cast

                    //CastSpell(hit.collider.gameObject.GetComponent<CharactersOfPlayers>().m_thisCharacter, m_currentSpellSelected);

                    //trouver un moyen si c'est multicible de prendre tout
                    hit.collider.gameObject.GetComponent<CharactersOfPlayers>().SpellCasted(m_currentSpellSelected);
                    UiManagerSession.instance.SpellUsed();
                }

                //une fois que le joueur a lanc� le sort il n'est plus s�lectionn�
                m_currentSpellSelected = null;
            }
        }
    }

    public void CastSpell(Character target, Spell spellToTarget)
    {
        target.CharactersLife -= spellToTarget.Damage;
        Debug.Log(target.CharactersName + target.CharactersLife);
        UiManagerSession.instance.SpellUsed();
        Debug.Log("Un sort � �t� cast�");
    }
}
