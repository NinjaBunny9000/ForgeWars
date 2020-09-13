using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeCoordinator : MonoBehaviour
{
    [Header("Control System")]
    [SerializeField] float moveRate = 2f;
    [SerializeField] float rotRate = 2f;
    enum Mode {FORGE, ANVIL, TUB};
    [SerializeField] Mode forgeMode = Mode.ANVIL;  // debug -> start in anvil for now
    enum State { RAW, HEATING, HEATED, HAMMERING, HAMMERED, DIPPING, DIPPED };
    [SerializeField] State forgeState = State.HEATED;  //* skipping heating/raw state for now

    [Header("Slack Tub")]
    public ParticleSystem tubSteam; //+ find where these should live
    public ParticleSystem tubSplash; 
    static float dipLength = 3;  // s
    float dipTimer = dipLength;

    [Header("Camera")]
    public GameObject mainCamera;
    Vector3 camAnvilPos = new Vector3(0,1.846f,-1.91f);
    Quaternion camAnvilRot = new Quaternion(0.19f,0,0,0.98f);
    Vector3 camTubPos = new Vector3(2.30900002f,1.75199997f,-1.78100002f);
    Quaternion camTubRot = new Quaternion(0.241711095f,-0.227521658f,0.0411190242f,0.942400575f);
    Vector3 camTargetPos;
    Quaternion camTargetRot;

    [Header("Tool")]
    public GameObject tool; //+ move this eventually
    Vector3 toolAnvilPos = new Vector3(0.497f,1.669f,-0.944f);
    Quaternion toolAnvilRot = new Quaternion(0,0,0,1);
    Vector3 toolPulledPos = new Vector3(0.476f,2.142f,-1.52f);
    Quaternion toolPulledRot = new Quaternion(-0.43f,-0.188f,0.3426f,0.814f);
    Vector3 toolTargetPos;
    Quaternion toolTargetRot;

    [Header("Weapon")]
    public GameObject weapon; //+ move this eventually
    Vector3 weaponAnvilPos = new Vector3(-0.114f,1.402f,-0.16f);
    Quaternion weaponAnvilRot = new Quaternion(-0.0013f,-0.4f,0f,0.92f);
    Vector3 weaponTubPos = new Vector3(1.98699999f,1.94299996f,-0.561999977f);
    Quaternion weaponTubRot = new Quaternion(-0.706948102f,0,0,0.707265496f);
    Vector3 weaponTargetPos;
    Quaternion weaponTargetRot;

    // Start is called before the first frame update
    void Start()
    {
        //* we're starting at anvil for now
        mainCamera.transform.position = camAnvilPos;
        mainCamera.transform.rotation = camAnvilRot;
        weapon.transform.position = weaponAnvilPos;
        weapon.transform.rotation = weaponAnvilRot;
        targetAnvil();
        tubSteam.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        moveIntoPosition();
        rotateIntoPosition();
        operateForge();
        progressModes();
    }

    private void operateForge()
    {
        switch (forgeMode) {
            case Mode.ANVIL:
                strikeWhenReady();
                break;

            case Mode.TUB:
                dipWhenReady();
                break;
        }
    }

    //+ new location for hammer movement/striking controls
    private void strikeWhenReady()
    {
        if (Input.GetMouseButtonDown(0)) {
            tool.gameObject.SendMessage("SwingTool");
        }
    }

    private void progressModes()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {

            switch (forgeMode) {
                case Mode.ANVIL:
                    forgeMode = Mode.TUB;  //* might need to compare locations and set when in pos
                    targetTub();
                    break;

                case Mode.TUB:
                    forgeMode = Mode.ANVIL; //* might need to compare locations and set when in pos
                    targetAnvil();
                    
                    break;
            }
        }
    }

    void dipWhenReady()
    {
        if (Input.GetMouseButtonDown(0)) {
            switch (forgeState) {
                case State.HAMMERED:
                    dipInTub();
                    forgeState = State.DIPPING;
                    break;

                case State.DIPPING:
                    if (dipTimer < Mathf.Epsilon) {
                        removeFromTub();
                        forgeState = State.DIPPED;
                        dipTimer = dipLength;  // reset the timer
                    }
                    break;
            }
        }

        if (forgeState == State.DIPPING) { dipTimer -= Time.deltaTime; }
    }

    //! called via string reference
    public void targetAnvil()
    {
        camTargetPos = camAnvilPos;
        camTargetRot = camAnvilRot;
        weaponTargetPos = weaponAnvilPos;
        weaponTargetRot = weaponAnvilRot;
        toolTargetPos = toolAnvilPos;
        toolTargetRot = toolAnvilRot;
    }

    //! called via string reference
    public void targetTub()
    {
        camTargetPos = camTubPos;
        camTargetRot = camTubRot;
        weaponTargetPos = weaponTubPos;
        weaponTargetRot = weaponTubRot;
        toolTargetPos = toolPulledPos;
        toolTargetRot = toolPulledRot;
    }

    void rotateIntoPosition()
    {
        Quaternion cameraRotation = mainCamera.transform.rotation;
        mainCamera.transform.rotation = Quaternion.Lerp(cameraRotation, camTargetRot, rotRate * Time.deltaTime);
        Quaternion weaponRot = weapon.transform.rotation;
        weapon.transform.rotation = Quaternion.Lerp(weaponRot, weaponTargetRot, moveRate * Time.deltaTime);
        Quaternion toolRot = tool.transform.rotation;
        tool.transform.rotation = Quaternion.Lerp(toolRot, toolTargetRot, moveRate * Time.deltaTime);
    }

    void moveIntoPosition()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        mainCamera.transform.position = Vector3.Lerp(cameraPosition, camTargetPos, moveRate * Time.deltaTime);
        Vector3 weaponPos = weapon.transform.position;
        weapon.transform.position = Vector3.Lerp(weaponPos, weaponTargetPos, moveRate * Time.deltaTime);
        Vector3 toolPos = tool.transform.position;
        tool.transform.position = Vector3.Lerp(toolPos, toolTargetPos, moveRate * Time.deltaTime);
    }

    void dipInTub()
    {
        weaponTargetPos = new Vector3(1.99f,1.32f,-0.562f);
        enableDipParticles();
    }

    // todo make this work on collision
    void enableDipParticles()
    {
        if (forgeState != State.DIPPING) {
            Debug.Log("dip particles");
            tubSteam.Stop();
            tubSteam.Play();
            tubSplash.Stop();
            tubSplash.Play();
        }
    }

    void removeFromTub() => weaponTargetPos = weaponTubPos;

    //! string ref
    public void WeaponForged() => forgeState = State.HAMMERED;
}
