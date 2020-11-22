using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private Vector2 _fingerDown;
    private DateTime _fingerDownTime;
    private Vector2 _fingerUp;
    private DateTime _fingerUpTime;

    private Vector2 _curPos;
    private bool _interacting;
    public bool mouseDebug = true;
    private Vector3 _lastEndPos;

    [HideInInspector] public PlayerSpell[] playerSpell;
    [SerializeField] Transform spawnPos;
    [SerializeField] Animator playerAnim;
    [SerializeField] AudioSource audioSource;
    [SerializeField][Range(1,10)] float playerTurnSpeed = 3;
    UIBar_Autorefill[] autorefills;
    Health_Player health;
    Plane plane;
    [SerializeField] Transform spellIconContainer;
    [SerializeField] UIBar_Autorefill spellPrefab;
    [SerializeField] Transform weaponHolder;

    private void Start()
    {
        List<UIBar_Autorefill> b = new List<UIBar_Autorefill>();

        JSONWeapon[] weapons = HandleJSON.GetAllEquippedWeapons();
        playerSpell = new PlayerSpell[weapons.Length - 1];
        bool equippedWeapon = false;
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].slot == 0)
            {
                //equip weapon
                Transform w = Instantiate(Resources.Load<Transform>("Weapons/" + weapons[i].item_name), weaponHolder);
                w.localPosition = Vector3.zero;
                w.localRotation = Quaternion.Euler(Vector3.zero);
                equippedWeapon = true;
            }
            else
            {
                //equip spell
                int index = i;
                if (equippedWeapon) index--;
                playerSpell[index] = Resources.Load<PlayerSpell>("Spells/" + weapons[i].item_name);
            }
        }

        for (int i = 0; i < playerSpell.Length; i++)
        {
            playerSpell[i].motion.Init();
            playerSpell[i].spell.Initialise(this.gameObject);
            UIBar_Autorefill bar = Instantiate(spellPrefab, spellIconContainer);
            bar.GetComponent<Image>().sprite = playerSpell[i].icon;
            bar.SetRefillSpeed(playerSpell[i].spell.timeBetweenUse);
            b.Add(bar);
        }

        autorefills = b.ToArray();

        plane = new Plane(Vector3.up, 0);
        health = GameObject.FindObjectOfType<Health_Player>();
        health.Init();
        health.InitHealth(100);
        UnitManager.instance.AddHealthPlayer(health);
    }

    private void Update()
    {
        if (health.IsDead) return;

        DetectMouseInteraction();
        DetectTouchInteraction();

        HandleInteraction();

        RotateToTarget();
    }

    void HandleInteraction()
    {
        if (_interacting)
        {
            _curPos = Input.mousePosition;
            for (int i = 0; i < playerSpell.Length; i++)
            {
                int motion = playerSpell[i].motion.MotionDetected(_fingerDown, _fingerUp, _curPos, (float)_fingerUpTime.Subtract(_fingerDownTime).TotalSeconds);
                if (motion != -1)
                {
                    Vector3 mP = Input.mousePosition;
                    if (mP.z < 0.1f)
                        mP.z = 0.1f;

                    float distance;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (plane.Raycast(ray, out distance))
                    {
                        _lastEndPos = ray.GetPoint(distance);
                    }

                    if (motion != -1)
                    {
                        if (autorefills[i].IsFull())
                        {
                            playerSpell[i].spell.TryUseSpell(spawnPos, playerAnim, new Vector3(_lastEndPos.x, spawnPos.position.y, _lastEndPos.z), audioSource);
                            autorefills[i].UpdateUI(1);
                        }
                    }

                    ResetInteracting();
                    break;
                }
            }
            if (!Input.GetMouseButton(0) && mouseDebug)
            {
                ResetInteracting();
            }
        }
    }

    public int Detect(DetectMotion m)
    {
        return m.MotionDetected(_fingerDown, _fingerUp, _curPos, (float)_fingerUpTime.Subtract(_fingerDownTime).TotalSeconds);
    }

    void RotateToTarget()
    {
        Transform player = playerAnim.transform.parent;
        Vector3 target = _lastEndPos;
        target.y = player.position.y;
        Quaternion rot = StaticMaths.GetLookRotation(target, player.position, player.forward, out bool shouldRotate, 1);
        if (shouldRotate)
        {
            player.rotation = Quaternion.Slerp(player.rotation, rot, Time.deltaTime * playerTurnSpeed);
        }
    }

    void DetectMouseInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _fingerDown = Input.mousePosition;
            _fingerUp = Input.mousePosition;
            _fingerDownTime = DateTime.Now;
            _interacting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _fingerDown = Input.mousePosition;
            _fingerUpTime = DateTime.Now;
            //  CheckSwipe();
        }
    }

    void DetectTouchInteraction()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerDown = touch.position;
                _fingerUp = touch.position;
                _fingerDownTime = DateTime.Now;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _fingerDown = touch.position;
                _fingerUpTime = DateTime.Now;
                //    CheckSwipe();
            }
        }
    }

    void ResetInteracting()
    {
        _interacting = false;
        _fingerUp = Vector2.zero;
        _fingerDown = Vector2.zero;

        for (int i = 0; i < playerSpell.Length; i++)
        {
            playerSpell[i].motion.Init();
        }
    }

}
