using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponForger : MonoBehaviour
{
    public GameObject collisionDetector;
    public GameObject metalBlank;
    public GameObject forgedWeapon;
    public Mesh metalBlankMesh;
    public Mesh forgedWeaponMesh;
    public Renderer blankRenderer;
    public Material blankMaterial;


    MeshCollider weaponCollider;
    public int hammerHits;
    public bool firstHit = true;

    public bool hammerHitting = false;

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = collisionDetector.GetComponent<MeshCollider>();

        metalBlankMesh = metalBlank.GetComponent<MeshFilter>().mesh;
        forgedWeaponMesh = forgedWeapon.GetComponent<MeshFilter>().mesh;
        weaponCollider.sharedMesh = metalBlankMesh;
        metalBlank.SetActive(true);
        forgedWeapon.SetActive(false);
        weaponCollider.transform.position = metalBlank.transform.position;
        weaponCollider.transform.rotation = metalBlank.transform.rotation;
        weaponCollider.transform.localScale = metalBlank.transform.localScale;

        blankRenderer = metalBlank.GetComponent<Renderer>();
        blankMaterial = blankRenderer.material;
        // blankMaterial.enableInstancing = true;
        blankMaterial.SetColor("_EmissionColor", Color.red);
        blankMaterial.EnableKeyword("_EMISSION");
    }



    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space)) {
    //         resetHits();
    //     }
    // }

    void resetHits()
    {
        metalBlank.SetActive(true);
        forgedWeapon.SetActive(false);
        weaponCollider.sharedMesh = metalBlankMesh;
        weaponCollider.transform.position = metalBlank.transform.position;
        weaponCollider.transform.rotation = metalBlank.transform.rotation;
        weaponCollider.transform.localScale = metalBlank.transform.localScale;
        blankMaterial.SetColor("_EmissionColor", Color.red);
        blankMaterial.EnableKeyword("_EMISSION");
        hammerHits = 0;
    }

    //! called via string reference
    public void registerHammerHit()  
    {

        if (hammerHitting) { return; }  
        hammerHitting = true;

        // bug 1 hit happens automatically for some reason
        if (firstHit) {
            firstHit = !firstHit;
            return;
        } else { hammerHits++; }
      
        if (hammerHits == 1) {
            blankMaterial.SetVector("_EmissionColor", Color.red * .3f);
        } else if (hammerHits == 2) {
            blankMaterial.SetVector("_EmissionColor", Color.red * .10f);
        } else if (hammerHits == 3) {
            blankMaterial.SetVector("_EmissionColor", new Color(0f,0f,0f));
        } else if (hammerHits == 4) {
            // move mesh to position of dagger
            metalBlank.SetActive(false);
            forgedWeapon.SetActive(true);
            weaponCollider.sharedMesh = forgedWeaponMesh;
            weaponCollider.transform.position = forgedWeapon.transform.position;
            weaponCollider.transform.rotation = forgedWeapon.transform.rotation;
            weaponCollider.transform.localScale = forgedWeapon.transform.localScale;
            SendMessage("WeaponForged");
        } else if (hammerHits == 5) {
            // resetHits();  // debug loop endlessly
        }
        
        blankMaterial.EnableKeyword("_EMISSION");  // needed to make sure the emission color updates next frame
    }

    //! called via string reference
    public void endHammerHit() => hammerHitting = false;

}
