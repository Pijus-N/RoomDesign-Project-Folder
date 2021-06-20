using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsInMainmenu : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private List<GameObject> allObjectsPrefabs;

    [SerializeField] private float customHeight = 0f;

    [SerializeField] private float maxSecondsBetweenSpawn = 0f;

    [SerializeField] private float minSecondsBetweenSpawn = 0f;

    private float timer;

    private bool isTimerComplete = false;
    private int Count;
    [SerializeField]private int maxCount=10;
    void Start()
    {
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (timer <= 0 && Count<maxCount)
		{
            SpawnObject();
            SetTimer();
		}
		else if(Count<maxCount)
		{
            CountDown();
		}
    }

    private void SpawnObject()
    {
        Count++;
        Vector3 offset = new Vector3(Random.Range(-1.5f, 1.5f), customHeight, Random.Range(-1.5f, 1.5f));

       GameObject obj = Instantiate(allObjectsPrefabs[Random.Range(0, allObjectsPrefabs.Count)], spawnPoint.position + offset, Quaternion.identity, spawnPoint.root);
        
        UnfreazeAllAxis(obj);

        PaintObject(obj);
	}

    private void CountDown()
	{
        timer -= Time.deltaTime;
		

	}

    private void SetTimer()
	{
        timer = Random.Range(minSecondsBetweenSpawn, maxSecondsBetweenSpawn);
    }

    private void UnfreazeAllAxis(GameObject obj)
	{
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }

	public void Reset()
	{
        Count = 0;
	}

	private void PaintObject(GameObject obj)
	{
        obj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
}
