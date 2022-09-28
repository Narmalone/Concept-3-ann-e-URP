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

    //Spell qui correspond au spell du boutton que le joueur a cliqué
    private Spell m_currentSpellSelected;

    private void Awake()
    {
        //création mini-singleton
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
            Debug.Log("Combat contre le groupe d'ennemis ID: " + GroupIdInFight + " a été lancé");

            //Si combat commence alors c'est le tour du joueur
            delTurn(true);
            delCombat(true);
            ManaManager.instance.SetFirstTurn();

            UiManagerSession.instance.CombatUi();

            //Set the ennmy team from the group we are fighting against
            m_ennemyTeam = m_currentFightingGroup.EnnemiesList;
        }
        else
        {
            Debug.LogError("Quelque chose à lancé un combat mais la value est à false ? " + boolValue);
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
            //Si joueur plus en combat on lui redonne le contrôle
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

    //Vérifier si c'est au tour du joueur
    //Sinon désactiver tous les boutons dans l'interfaces du joueur
    //Si le spell a déjà été utilisé le spell resteras bloqué
    private bool IsPlayerTurn(bool boolValue)
    {
        if (boolValue)
        {
            //Si tour du joueur
            //Interface joueur activée et l'IA ne joue pas
            UiManagerSession.instance.PlayerTurn();
        }
        else
        {
            //si ce n'est pas le temps du joueur
            //désactiver l'interface puis c'est au tour de l'IA
            ManaManager.instance.NextTurn();
            UiManagerSession.instance.NotPlayerTurn();

            if (isFighting)
            {
                //Ai manager pour faire jouer l'IA
                AiManager.instance.SetEnnemies();
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
            //Désactiver le bouton sort que le joueur à lancé
            delTurn(true);
        }
       
    }

    //Fonction qui permmet de savoir qu'est-ce qui est une cible ou pas dans le sort avant de le cast
    public void CheckSpellTarget(Spell spellCliqued)
    {
        //Une fois que le check est fait dire quelles sont les target possibles en fonction des boolens
        //mettre un check dans les ennemies ? Genre si y'a que notre équipe qui est targetable bah pas les ennemis ? -> avec un foreach ennemies in list
        //Si Un seul ennemi est targetable comment faire ? 
        m_currentSpellSelected = spellCliqued;
        //trouver comment avoir le bon groupe des ennemis
        if (m_currentSpellSelected.IsSoloTarget && m_currentSpellSelected.IsEnemyTarget)
        {
            //Multiples cibles et notre équipe par exemple le sort de heal
            //Ce genre de sort on le Cast direct car cela touche toute les cibles

            //FAIRE LE CHECK DANS L'INTERFACE SPELL PLUTOT ??? mais dans ce cas comment comprendre quel spell en fonction du bouton
            Debug.Log("Plusieures cibles de notre équipe");
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
            //Si une seule cible et que c'est notre équipe
            Debug.Log("Une seule cible de notre équipe");
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
            //Si une seule cible et que c'est celle de l'équipe ennemie
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
    public void CheckMobGroup()
    {
        if(m_enemyList.Count == 0)
        {
            m_currentSpellSelected = null;
            delCombat(false);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Si le joueur n'a rien sélectionné on ne fait rien
            if (m_currentSpellSelected == null) return;

            //raycast quand le joueur clique envoyer le sort à la target
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, m_enemyMask))
            {
                
                //Check si la cible est targetable ou pas réfléchir à un autre moyen pour vérifier
                if(hit.collider.gameObject.GetComponent<Ennemy>().isTargetable == true)
                {

                    if (m_currentSpellSelected.IsSoloTarget)
                    {
                        List<Ennemy> ennemyToRemove = new List<Ennemy>();
                        //Cette ligne
                        foreach(Ennemy mob in m_enemyList)
                        {
                            if (!mob.SpellCasted(m_currentSpellSelected)) { ennemyToRemove.Add(mob); } ;
                            
                            Debug.Log(mob.name);
                        }

                        foreach(Ennemy mob in ennemyToRemove)
                        {
                            m_enemyList.Remove(mob);
                        }

                        if(m_enemyList == null)
                        {
                            delCombat(false);
                            delTurn(false);
                        }
                    }
                    else
                    {
                        Ennemy hited;
                        hited = hit.collider.gameObject.GetComponent<Ennemy>();
                        if (!hited.SpellCasted(m_currentSpellSelected)) { m_enemyList.Remove(hited); }
                    }

                    //si la cible est morte sur ce coup
                    CheckMobGroup();

                    UiManagerSession.instance.SpellUsed();

                    delTurn(false);

                }

                //une fois que le joueur a lancé le sort il n'est plus sélectionné
                m_currentSpellSelected = null;
            }
            
            else if (Physics.Raycast(ray, out hit, 1000, m_charactersMask))
            {
                
                //Check si la cible est targetable ou pas réfléchir à un autre moyen pour vérifier
                if(hit.collider.gameObject.GetComponent<CharactersOfPlayers>().isTargetable == true)
                {
                    List<CharactersOfPlayers> charaToRemove = new List<CharactersOfPlayers>();

                    //trouver un moyen si c'est multicible de prendre tout
                    if (m_currentSpellSelected.IsSoloTarget && m_currentSpellSelected.IsEnemyTarget)
                    {
                        foreach(CharactersOfPlayers team in m_ourTeam)
                        {
                            team.SpellCasted(m_currentSpellSelected);
                        }
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<CharactersOfPlayers>().SpellCasted(m_currentSpellSelected);
                    }
                    //juste appeller le Ai manager CastSpell
                    UiManagerSession.instance.SpellUsed();
                    delTurn(false);
                }

                //une fois que le joueur a lancé le sort il n'est plus sélectionné
                m_currentSpellSelected = null;
            }
        }
    }
}
