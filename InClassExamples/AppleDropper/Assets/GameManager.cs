using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numBaskets;
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private float basketSpacing;
    [SerializeField] private float botBasket;
    public int score;
    public Text text;
    private List<GameObject> basketList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject basket = Instantiate(basketPrefab) as GameObject;
            Vector3 pos = Vector3.zero;
            pos.y = botBasket + (basketSpacing * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
