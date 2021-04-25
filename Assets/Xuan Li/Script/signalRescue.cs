using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Health))]
public class signalRescue : MonoBehaviour
{
    
    private Health health;
    
    [SerializeField]
    private float signalPercentage;
    [SerializeField] 
    private float rescueRadius;
    [SerializeField] 
    private List<GameObject> callableFriends;
    [SerializeField] 
    private int cooldown = 10;
    [SerializeField] 
    private float timer;
    public GameObject rescuer;

        // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "Helper")
        {
            checkHPPercentage();
        }
    }

    void checkHPPercentage()
    {
        
        float currentPercent = health.GetCurrentHealth() / health.getMaxHealth();
        //float currentPercent = hp / maxHP;

        if (currentPercent <= signalPercentage && Time.time>timer && rescuer == null)
        {
            signal();
            timer = Time.time + cooldown;
        }
    }
    
    void signal()
    {
        callableFriends.Clear();
        GameObject[] friends = GameObject.FindGameObjectsWithTag("Helper");
        //Collider[] reachableFriends = Physics.OverlapSphere(transform.position, rescueRadius);
        if (friends.Length > 0)
        {
            for (int i = 0; i < friends.Length; i++)
            {
                if((friends[i].transform.position-transform.position).magnitude<=rescueRadius && (friends[i].transform.position-transform.position).magnitude!=0 && friends[i].GetComponent<rescue>().available)
                    callableFriends.Add(friends[i]);
            }
            rescue res = callableFriends[0].GetComponent<rescue>();
            res.patient = gameObject.transform;
            res.goRescue();
            rescuer = callableFriends[0];
        }
        
    }
}
