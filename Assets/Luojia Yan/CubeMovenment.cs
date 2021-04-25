using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovenment : MonoBehaviour
{
    public GameObject cube0;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public GameObject cube9;
    public GameObject[] cubes;
    public float speed = 0.19f;
    Vector3 point0_1;
    Vector3 point0_2;
    Vector3 point1_1;
    Vector3 point1_2;
    Vector3 point2_1;
    Vector3 point2_2;
    Vector3 point3_1;
    Vector3 point3_2;
    Vector3 point4_1;
    Vector3 point4_2;
    Vector3 point5_1;
    Vector3 point5_2;
    Vector3 point6_1;
    Vector3 point6_2;
    Vector3 point7_1;
    Vector3 point7_2;
    Vector3 point8_1;
    Vector3 point8_2;
    Vector3 point9_1;
    Vector3 point9_2;
    Vector3 point10_1;
    Vector3 point10_2;

    // Start is called before the first frame update
    void Start()
    {
        point0_1 = new Vector3(300, 25, 200);
        point0_2 = new Vector3(-300, 25, 200);

        point1_1 = new Vector3(300, 25, 50);
        point1_2 = new Vector3(-300, 25, 50);

        point2_1 = new Vector3(300, 25, -100);
        point2_2 = new Vector3(-300, 25, -100);

        point3_1 = new Vector3(300, 25, -250);
        point3_2 = new Vector3(-300, 25, -250);

        point4_1 = new Vector3(300, 25, -400);
        point4_2 = new Vector3(-300, 25, -400);








        point5_1 = new Vector3(-250, 35, -400);
        point5_1 = new Vector3(-250, 35, 300);

        point6_1 = new Vector3(-100, 35, -400);
        point6_1 = new Vector3(-100, 35, 300);

        point7_1 = new Vector3(0, 35, -400);
        point7_2 = new Vector3(0, 35, 300);

        point8_1 = new Vector3(125, 35, -400);
        point8_2 = new Vector3(125, 35, 300);

        point9_1 = new Vector3(300, 35, -400);
        point9_2 = new Vector3(300, 35, 400);



    }

    // Update is called once per frame
    void Update()
    {
        cubes[0] = cube0;
        cubes[1] = cube1;
        cubes[2] = cube2;
        cubes[3] = cube3;
        cubes[4] = cube4;
        cubes[5] = cube5;
        cubes[6] = cube6;
        cubes[7] = cube7;
        cubes[8] = cube8;
        cubes[9] = cube9;


        float time = Mathf.PingPong(Time.time * speed*0.05f, 1);
        cubes[0].transform.position = Vector3.Lerp(point0_1, point0_2, time);
        cubes[1].transform.position = Vector3.Lerp(point1_1, point1_2, time);
        cubes[2].transform.position = Vector3.Lerp(point2_1, point2_2, time);
        cubes[3].transform.position = Vector3.Lerp(point3_1, point3_2, time);
        cubes[4].transform.position = Vector3.Lerp(point4_1, point4_2, time);
        cubes[5].transform.position = Vector3.Lerp(point5_1, point5_2, time);
        cubes[6].transform.position = Vector3.Lerp(point6_1, point6_2, time);
        cubes[7].transform.position = Vector3.Lerp(point7_1, point7_2, time);
        cubes[8].transform.position = Vector3.Lerp(point8_1, point8_2, time);
        cubes[9].transform.position = Vector3.Lerp(point9_1, point9_2, time);


    }
}
