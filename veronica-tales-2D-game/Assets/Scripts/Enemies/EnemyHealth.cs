using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   [SerializeField] private int startingHealth = 3;
   [SerializeField] private GameObject deathVFXPrefab;

   private int currentHealth;
   private Knockback knockback;
   private Flash flash;

   private void Awake() {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
   }

   private void Start(){
    currentHealth = startingHealth;
   }

   public void TakeDamage(int damageAmount){
       currentHealth -= damageAmount;
       knockback.GetKnockedBack(PlayerController.Instance.transform, 10f);
       StartCoroutine(flash.FlashRoutine());
       StartCoroutine(CheckDetectDeathRoutine());
   }

   private IEnumerator CheckDetectDeathRoutine(){
       yield return new WaitForSeconds(flash.GetRestoreMatTime());
       DetectHealth();
   }

   private void DetectHealth(){
       if(currentHealth <= 0){
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
       }
   }
}
