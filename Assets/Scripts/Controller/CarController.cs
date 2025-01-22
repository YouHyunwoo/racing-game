using System.Collections;
using System.Collections.Generic;
using RacingGame.Model;
using UnityEngine;
using UnityEngine.Events;

public class CarController : MonoBehaviour
{
    [SerializeField] Transform roadFieldTransform;
    [SerializeField] GameObject carModel;
    [SerializeField] UnityEvent<bool> onLeftSideActive;
    [SerializeField] UnityEvent<bool> onRightSideActive;
    [SerializeField] UnityEvent<float> onGasolineConsume;
    [SerializeField] UnityEvent onGasolineEmpty;

    float halfScreenWidth;
    Car car;
    GameObject carObject;

    public void Initialize()
    {
        enabled = false;
    }

    public void Stop()
    {
        enabled = false;
        StopAllCoroutines();
    }

    public void Clear()
    {
        Destroy(carObject);
        car = null;
    }

    public void Play(Car car)
    {
        halfScreenWidth = Camera.main.pixelWidth / 2;
        this.car = car;
        GenerateCarObject();
        StartCoroutine(ConsumeGasolineRoutine());
        enabled = true;
    }

    void GenerateCarObject()
    {
        carObject = Instantiate(carModel, new Vector3(0, 0, 4), Quaternion.identity, roadFieldTransform);
    }

    IEnumerator ConsumeGasolineRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            car.Gasoline -= car.GasolineEfficiency * 0.1f;
            onGasolineConsume.Invoke(car.Gasoline);
            if (car.Gasoline <= 0)
            {
                onGasolineEmpty.Invoke();
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < halfScreenWidth)
            {
                carObject.transform.position += new Vector3(-1, 0, 0) * car.HorizontalMovementSpeed * Time.deltaTime;
                onLeftSideActive.Invoke(true);
                onRightSideActive.Invoke(false);
            }
            else {
                carObject.transform.position += new Vector3(+1, 0, 0) * car.HorizontalMovementSpeed * Time.deltaTime;
                onLeftSideActive.Invoke(false);
                onRightSideActive.Invoke(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            onLeftSideActive.Invoke(false);
            onRightSideActive.Invoke(false);
        }
    }
}
