using System.Collections;
using System.Collections.Generic;
using RacingGame.Model;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] DataController dataController;
    [SerializeField] Transform roadFieldTransform;
    [SerializeField] GameObject itemModel;
    [SerializeField] float horizontalRange;

    Car car;
    List<GameObject> itemList = new List<GameObject>();

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
        foreach (var itemObject in itemList)
        {
            Destroy(itemObject);
        }
        itemList.Clear();
        car = null;
        enabled = false;
    }

    public void Play(Car car)
    {
        this.car = car;
        enabled = true;
        StartCoroutine(GenerationRoutine());
    }

    IEnumerator GenerationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(dataController.itemGenerationInterval);

            if (Random.value >= dataController.itemGenerationRate) { continue; }

            var rows = dataController.rows;
            var rowWidth = horizontalRange / rows;
            var randomRow = Random.Range(0, rows);
            var rowPositionX = randomRow * rowWidth + rowWidth / 2f - horizontalRange / 2f;
            var itemObject = Instantiate(itemModel, new Vector3(rowPositionX, 0, 40), Quaternion.identity, roadFieldTransform);
            var oil = itemObject.GetComponent<Oil>();
            oil.onHit += () => { OnOilHit(oil); };
            itemList.Add(itemObject);
        }
    }

    void Update()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            var itemObject = itemList[i];
            itemObject.transform.position -= new Vector3(0, 0, car.VerticalMovementSpeed * Time.deltaTime);
            if (itemObject.transform.position.z <= 0)
            {
                Destroy(itemObject);
                itemList.RemoveAt(i);
                i--;
            }
        }
    }

    void OnOilHit(Oil oil)
    {
        car.Gasoline = Mathf.Clamp(car.Gasoline + dataController.itemGasolineRecovery, 0, 100);
        Destroy(oil.gameObject);
        itemList.Remove(oil.gameObject);
    }
}
