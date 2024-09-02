using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndExplosionController : MonoBehaviour
{
    public GameObject tankExplosion;
    public GameObject playerTank;

    public void TankExplosion()
    {
        Vector2 spawnPosition = playerTank.transform.position;
        GameObject explosionInstance = Instantiate(tankExplosion, spawnPosition, Quaternion.identity);
        StartCoroutine(WaitExplosion(explosionInstance));
    }

    IEnumerator WaitExplosion(GameObject explosionInstance)
    {
        yield return new WaitForSeconds(0.20f);
        Destroy(explosionInstance);
        yield return new WaitForSeconds(1000000.0f);
    }
}
