using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarketHandler : MonoBehaviour
{

    public GameObject weaponsForSale;
    public TextMeshProUGUI text;


    void Awake()
    {
        // text = weaponsForSale.GetComponent<TextMeshProUGUI>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        ListItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ListItems()
    {
        // text.SetText("TESTING");
    }
}
