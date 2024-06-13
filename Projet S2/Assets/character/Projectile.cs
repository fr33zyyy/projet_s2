using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 1;
    public GameObject projectile;
    public Transform firePoint;
    private Transform playerTransform;
    private float timefire = 2f;
    private float firerate = 4;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > timefire)
        {
            timefire = Time.time + 1/firerate;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 playerForward = playerTransform.forward;

        // Décaler la position de spawn pour qu'elle soit juste devant le joueur
        Vector3 spawnPosition = playerPosition + playerForward * 2f + Vector3.up; // Vector3.up pour déplacer légèrement le projectile au-dessus du sol

        Quaternion spawnRotation = firePoint.rotation;

        // Instancier le projectile à la position calculée
        var projectileObj = Instantiate(projectile, spawnPosition, spawnRotation) as GameObject;

        // Appliquer la vitesse au projectile dans la direction du joueur
        projectileObj.GetComponent<Rigidbody>().velocity = playerForward * projectileSpeed;

        // Assigner les dégâts au script du projectile
        bouledefeu projectileDamage = projectileObj.GetComponent<bouledefeu>();
        if (projectileDamage != null)
        {
            projectileDamage.damage = projectileDamage;
        }
    }
}