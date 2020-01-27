using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private float treeSpeed;
    [SerializeField] private float minMaxValue;
    [SerializeField] private float chanceDirChange;
    [SerializeField] private GameObject applePrefab;
    public float timeBetweenDrops;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DropApple", 1f, timeBetweenDrops);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += treeSpeed * Time.deltaTime;
        transform.position = pos;

        if (Mathf.Abs(transform.position.x) >= minMaxValue
            || Random.value < chanceDirChange)
        {
            treeSpeed *= -1;
        }
    }

    void DropApple()
    {
        GameObject apple = Instantiate(applePrefab) as GameObject;
        apple.transform.position = transform.position;
    }
}
