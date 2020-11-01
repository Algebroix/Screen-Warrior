using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to cache bullets since they must appear frequently in high numbers by different entities. Add a child Bullet in editor to use.
public class BulletPool : MonoBehaviour
{
    private List<Bullet> bullets;
    private Transform poolTransform;

    public Bullet GetBullet()
    {
        //Find first inactive bullet and return it.
        int bulletIndex = 0;
        while (bullets[bulletIndex].isActiveAndEnabled)
        {
            bulletIndex++;
            //If checked all bullets, create new.
            if (bulletIndex >= bullets.Count)
            {
                Bullet newBullet = Instantiate(bullets[0], poolTransform);
                bullets.Add(newBullet);
                return newBullet;
            }
        }
        //If some is unused, return it.
        return bullets[bulletIndex];
    }

    private void Awake()
    {
        //Cache
        bullets = new List<Bullet>(GetComponentsInChildren<Bullet>(true));
        poolTransform = transform;
    }
}