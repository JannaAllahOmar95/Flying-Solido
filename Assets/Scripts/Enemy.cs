using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyText enemyText;
    private EnemiesSpawn enemiesSpawn;

    public int maxHealth = 100;
    internal int currentHealth;
    public bool isPlayerOnSight;
    public float attackRange;
    public LayerMask playerLayer;
    public Transform player;
    public int speed = 1;
    public bool canattack;
    public bool canChase;
    public float chaseRange;
    public bool waitKill = true;

    private void Start()
    {
        enemyText = EnemyText.Instance;
        enemiesSpawn = EnemiesSpawn.Instance;
        currentHealth = maxHealth;
        UpdateEnemyText();
    }

    private void Update()
    {
        canattack = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        canChase = Physics.CheckSphere(transform.position, chaseRange, playerLayer);

        if (canattack == true)
        {
            AttackPlayer();
        }
        else if (canChase == true && canattack == false)
        {
            ChasePlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(10);
        }
    }

    private void AttackPlayer()
    {
        if (waitKill == true)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange + 2, playerLayer))
            {
                waitKill = false;
                hit.transform.gameObject.GetComponent<Health>().TakeDamage(1);
                StartCoroutine(waitBeforeKill());
            }
        }
    }

    private IEnumerator waitBeforeKill()
    {
        yield return new WaitForSeconds(1);
        waitKill = true;
    }

    public void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), speed * Time.deltaTime);
    }

    public void DestroyEnemy()
    {
        if (currentHealth <= 0)
        {
            enemiesSpawn.OnEnemyDestroyed();

            Destroy(gameObject);
        }
    }

    public void UpdateEnemyText()
    {
        Debug.Log(currentHealth);

        enemyText.gameObject.GetComponent<TextMeshProUGUI>().text = "Enemy HP: " + currentHealth.ToString();
    }
}