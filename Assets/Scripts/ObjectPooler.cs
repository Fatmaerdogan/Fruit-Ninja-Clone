using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] List<PooledObject> _pooledObjects;

    List<Queue<Target>> _pools;

    public static ObjectPooler Instance;

    private void Awake()=>Instance = this;

    [System.Serializable]
    public class PooledObject
	{
        [SerializeField] Target _prefab;
        [SerializeField] int _size;

        public Target Prefab { get => _prefab; }
        public int Size { get => _size; }
	}

    void Start()
    {
        _pools = new List<Queue<Target>>();

        foreach (PooledObject pooledObject in _pooledObjects)
		{
            Queue<Target> pool = new Queue<Target>();

            for (int i = 0; i < pooledObject.Size; i++)
			{
                Target obj = Instantiate(pooledObject.Prefab);
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(transform);
                pool.Enqueue(obj);
			}

            _pools.Add(pool);
		}
    }

    public Target GetFromPool(int poolIndex)
	{
        Target objectToSpawn = _pools[poolIndex].Dequeue();

        objectToSpawn.transform.position = transform.position;
        objectToSpawn.gameObject.SetActive(true);

        _pools[poolIndex].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public int PoolSize()
	{
        return _pooledObjects.Count;
	}
}
