using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public Queue<GameObject> targetList = new Queue<GameObject>();

    public Transform target;

    public float arriveRadius=2f;
    public float restTime = 3f;
    public float velocity = 3f;

    public bool arrived = false;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp;
        temp = targetList.Dequeue();
        target = temp.transform;
        targetList.Enqueue(temp);
    }

    // Update is called once per frame
    void Update()
    {
        switchTarget();
    }

    public void switchTarget()
    {
        timer += Time.deltaTime;
        if ((target.position - transform.position).magnitude < arriveRadius)
        {
            arrived = true;
            timer = 0;
            
        }
        if (arrived && timer>restTime)
        {
            GameObject temp;
            temp = targetList.Dequeue();
            target = temp.transform;
            targetList.Enqueue(temp);
            arrived = false;
        }

        if (!arrived)
        {
            patrolAround();
        }
    }

    void patrolAround()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.forward = direction;
        transform.position += direction * (velocity * Time.deltaTime);
    }
}
