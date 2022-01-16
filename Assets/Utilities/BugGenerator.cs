using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BugGenerator : Singleton<BugGenerator>
{
    [SerializeField] private int flyCount;
    [SerializeField] private int fireBugCount;
    [SerializeField] private GameObject[] flies;
    [SerializeField] private GameObject[] fireBugs;
    private float minX = -50f;
    private float maxX = 50f;
    private float minY = 0f;
    private float maxY = 2.3f;

    public int FlyCount { get => flyCount; }

    private void Awake()
    {
        Assert.IsNotNull(flies);
        Assert.IsNotNull(fireBugs);
    }

    private float generateXPos()
    {
        float randomXPos = Random.Range(minX, maxX);
        return randomXPos;
    }

    private float generateYPos()
    {
        float randomYPos = Random.Range(minY, maxY);
        return randomYPos;
    }

    private void instantiateFlies()
    {
        int index = Random.Range(0, flies.Length);
        float x = generateXPos();
        float y = generateYPos();
        //Instantiate(availableGems[index], new Vector3(x, y, z), Quaternion.identity);
        Instantiate(flies[index], new Vector3(x, y, 0),Quaternion.identity);
        //print("fly instantiated");
    }

    private void instantiateFireBugs()
    {
        int index = Random.Range(0, fireBugs.Length);
        float x = generateXPos();
        float y = generateYPos();
        //Instantiate(availableGems[index], new Vector3(x, y, z), Quaternion.identity);
        Instantiate(fireBugs[index], new Vector3(x, y, 0), Quaternion.identity);
        print("fireBug instantiated");
    }
   
    void Start()
    {

        for (int i = 0; i < flyCount; i++)
        {
            instantiateFlies();
        }
        for(int i=0; i< fireBugCount; i++)
        {
            instantiateFireBugs();
        }
    }

   
    void Update()
    {
        
    }
}
