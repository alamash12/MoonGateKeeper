using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // 게임프리펩을 담아두는 리스트
    [SerializeField] private List<GameObject> objectPrefebList = new List<GameObject>();
    // 프리펩의 수만큼 큐를 생성
    private List<Queue<GameObject>> poolingObjectQueueList = new List<Queue<GameObject>>();

    private void Start()
    {
        for (int i = 0; i < objectPrefebList.Count; i++)
        {
            poolingObjectQueueList.Add(new Queue<GameObject>());
        }
        Initialize(10);
    }
    private GameObject createNewObject(int prefebID)
    {
        GameObject newObj = Instantiate(objectPrefebList[prefebID]);
        newObj.transform.SetParent(Instance.transform);
        newObj.SetActive(false);
        return newObj;
    }
    private void Initialize(int count)
    {
        int prefebNum = objectPrefebList.Count;
        for (int i = 0; i < prefebNum; i++)
        {
            for (int j = 0; j < count; j++)
            {
                poolingObjectQueueList[i].Enqueue(createNewObject(i));
            }
        }
    }
    public static GameObject GetObject(int prefebID)
    {
        //Debug.Log($"{prefebID}번 오브젝트 생성");
        // Pool에 남는 GameObject가 있을 때 남는 Object return
        if (Instance.poolingObjectQueueList[prefebID].Count > 0)
        {
            GameObject obj = Instance.poolingObjectQueueList[prefebID].Dequeue();
            //obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }
        // Pool에 남는 GameObject가 없을 떄.. 새로 Pool에 생성 후 return
        else
        {
            GameObject newObj = Instance.createNewObject(prefebID);
            //newObj.transform.SetParent(null);
            newObj.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnObject(GameObject obj, int prefebID)
    {
        obj.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueueList[prefebID].Enqueue(obj);
    }
}
