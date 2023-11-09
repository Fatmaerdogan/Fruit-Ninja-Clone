using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Option")]
    [SerializeField] SpawnOption spawnOption = SpawnOption.RandomWave;
    [SerializeField] private Vector2 minMaxTargetsInAllAtOnce;
    [SerializeField] private Vector2 minMaxTargetsInOneByOne ;

    [Header("Spawn Rate In Seconds")]
    [SerializeField] private Vector2 minMaxSpawnRateInAllAtOnce;
    [SerializeField] private Vector2 minMaxSpawnRateInOneByOne;
    [SerializeField] private Vector2 minMaxSpawnRateInOneAtOnce;


    [Header("Forces Values Applied to Objects at Spawn")]
    [SerializeField] private Vector2 minMaxVerticalForce ;
    [SerializeField] float maxHorizontalForce,maxTorque = 10f;

    void Start()
    {
        StartCoroutine(SelectSpawnOption());
    }

	IEnumerator SelectSpawnOption()
	{
        while (true)
        {
            SpawnOption randomSpawnOption = spawnOption;
            if (spawnOption == SpawnOption.RandomWave)
            {
                randomSpawnOption = spawnOption.RandowmWave();
            }

            Coroutine selectedCoroutine;
            if (randomSpawnOption == SpawnOption.WaveAllAtOnce)
            {
                selectedCoroutine = StartCoroutine(SpawnWaveAllAtOnce());
            }
            else if (randomSpawnOption == SpawnOption.WaveOneByOne)
			{
                selectedCoroutine = StartCoroutine(SpawnWaveOneByOne());
            }
            else // SpawnOption.OneAtOnce
            {
                selectedCoroutine = StartCoroutine(SpawnOneAtOnce());
            }

            yield return selectedCoroutine; // wait for selected coroutine
        }
	}

    IEnumerator SpawnWaveAllAtOnce()
	{
        yield return new WaitForSeconds(Random.Range((int)minMaxSpawnRateInAllAtOnce.x, (int)minMaxSpawnRateInAllAtOnce.y));

        if (GameManager.instance.State == State.Playing)
        {
            int numberOfTargetsToSpawn = Random.Range((int)minMaxTargetsInAllAtOnce.x, (int)minMaxTargetsInAllAtOnce.y);

            Vector3 randomVerticalForce = RandomVerticalForce();
            for (int i = 0; i < numberOfTargetsToSpawn; i++)
			{
                int randomTargetIndex = Random.Range(0, ObjectPooler.Instance.PoolSize());

                Target newTarget = ObjectPooler.Instance.GetFromPool(randomTargetIndex);

                newTarget.ResetForces();
                newTarget.AddForce(randomVerticalForce);
                newTarget.AddRandomHorizontalForce(maxHorizontalForce);
                newTarget.AddRandomTorque(maxTorque);
            }
        }
    }

    IEnumerator SpawnWaveOneByOne()
	{
        int numberOfTargetsToSpawn = Random.Range((int)minMaxTargetsInOneByOne.x, (int)minMaxTargetsInOneByOne.y);
        for (int i = 0; i < numberOfTargetsToSpawn; i++)
	    {
            yield return new WaitForSeconds(Random.Range((int)minMaxSpawnRateInOneByOne.x, (int)minMaxSpawnRateInOneByOne.y));

            if (GameManager.instance.State == State.Playing)
            {
                int randomTargetIndex = Random.Range(0, ObjectPooler.Instance.PoolSize());

                Target newTarget = ObjectPooler.Instance.GetFromPool(randomTargetIndex);

                newTarget.ResetForces();
                newTarget.AddRandomForce((int)minMaxVerticalForce.x, (int)minMaxVerticalForce.y, maxHorizontalForce);
                newTarget.AddRandomTorque(maxTorque);
            }
        }
    }

    IEnumerator SpawnOneAtOnce()
	{
        yield return new WaitForSeconds(Random.Range((int)minMaxSpawnRateInOneAtOnce.x, (int)minMaxSpawnRateInOneAtOnce.y));
        if (GameManager.instance.State == State.Playing)
        {
            int randomTargetIndex = Random.Range(0, ObjectPooler.Instance.PoolSize());

            Target newTarget = ObjectPooler.Instance.GetFromPool(randomTargetIndex);

            newTarget.ResetForces();
            newTarget.AddRandomForce((int)minMaxVerticalForce.x, (int)minMaxVerticalForce.y, maxHorizontalForce);
            newTarget.AddRandomTorque(maxTorque);
        }
    }

    Vector3 RandomVerticalForce() => Vector3.up * Random.Range((int)minMaxVerticalForce.x, (int)minMaxVerticalForce.y);
}
public static class SpawnOptionMenthods
{
    public static SpawnOption RandowmWave(this SpawnOption spawnOption)
    {

        int randomInt = Random.Range(0, 2);

        if (randomInt == 0)
            return SpawnOption.WaveAllAtOnce;
        else
            return SpawnOption.WaveOneByOne;
    }
}