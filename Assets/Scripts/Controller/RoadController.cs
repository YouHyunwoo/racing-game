using System.Collections;
using System.Collections.Generic;
using RacingGame.Model;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] Transform roadEnvironmentTransform;
    [SerializeField] GameObject roadModel;

    Car car;
    List<GameObject> roadList = new List<GameObject>();

    public void Initialize()
    {
        enabled = false;
    }

    public void Stop()
    {
        enabled = false;
    }

    public void Clear()
    {
        foreach (var roadObject in roadList)
        {
            Destroy(roadObject);
        }
        roadList.Clear();
        car = null;
    }

    public void Play(Car car)
    {
        this.car = car;
        GenerateRoad();
        enabled = true;
    }

    void GenerateRoad()
    {
        for (int i = 0; i < 2; i++)
        {
            var roadObject = Instantiate(roadModel, new Vector3(0, 0, i * 40), Quaternion.identity, roadEnvironmentTransform);
            roadList.Add(roadObject);
        }
    }

    void Update()
    {
        for (int i = 0; i < roadList.Count; i++)
        {
            var roadObject = roadList[i];
            roadObject.transform.position -= new Vector3(0, 0, car.VerticalMovementSpeed * Time.deltaTime);
            if (roadObject.transform.position.z <= -40f)
            {
                roadObject.transform.position += new Vector3(0, 0, 80f);
            }
        }
    }
}
