using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject _hitEffect;
    [SerializeField] float _damage;
    [SerializeField] bool _isNapalm = false;
    [SerializeField] bool _isRocket = false;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cell"))
        {
            int rand = Random.Range(0, 100);
            if (rand > GameManager.Truck.Avoidance)
            {
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
                if (_isNapalm)
                {

                }
                else if (_isRocket)
                {
                    Debug.Log("[Bullet.cs]rocket booooommmmbb!!!!");
                    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5, Vector3.zero, 10, LayerMask.GetMask("Cell"));
                    List<RoomSystem> rooms = new List<RoomSystem>();
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (rooms.Contains(hit.collider.transform.parent.GetComponent<RoomSystem>()))
                        {
                            continue;
                        }
                        else
                        {
                            rooms.Add(hit.collider.transform.parent.GetComponent<RoomSystem>());
                        }
                    }
                    Debug.Log(rooms.Count);
                    foreach (RoomSystem room in rooms)
                    {
                        room.TakeHit(_damage);
                    }
                }
                else
                {

                    collision.transform.parent.GetComponent<RoomSystem>().TakeHit(_damage);

                }
                Destroy(gameObject);
            }
            else
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
        else if (collision.gameObject.CompareTag("EnemyRoom") || collision.gameObject.CompareTag("EnemyEngine"))
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
            if (_isNapalm)
            {
                Debug.Log("[Bullet.cs]neiparm booooommmmbb!!!!");

                if (collision.gameObject.CompareTag("EnemyRoom"))
                {
                    collision.GetComponent<EnemyRoom>().TakeNapalm(_damage);
                }
            }
            else if (_isRocket)
            {
                Debug.Log("[Bullet.cs]rocket booooommmmbb!!!!");

                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5, Vector2.up, 10, LayerMask.GetMask("Enemy"));
                Debug.Log("Number of Hits " + hits.Length);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.CompareTag("EnemyRoom"))
                    {
                        hit.collider.GetComponent<EnemyRoom>().TakeDamage(_damage);
                    }
                    else
                    {
                        hit.collider.GetComponent<EnemyEngine>().TakeDamage(_damage);
                    }
                }
                Destroy (gameObject);   
            }
            else
            {
                if (collision.gameObject.CompareTag("EnemyRoom"))
                {
                    collision.GetComponent<EnemyRoom>().TakeDamage(_damage);
                }
                else
                {
                    collision.GetComponent<EnemyEngine>().TakeDamage(_damage);
                }
            }
            Destroy(gameObject);
        }
    }
}
