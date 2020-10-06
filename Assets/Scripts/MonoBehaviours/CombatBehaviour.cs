using System.Collections;
using UnityEngine;

public class CombatBehaviour : MonoBehaviour
{
    public enum FactionEnum {Good,Bad,Ugly}

    public FactionEnum faction;
    public FloatData health;
    [SerializeField] private AttackData triggerAttack;

    private MeshRenderer meshRenderer;
    private PlayerBehaviour player;
    private EnemyBehaviour enemy;
    private bool isPlayer, isEnemy;

    [SerializeField] private float invincibleTime = 1f;
    private WaitForSeconds invincibleTimeWait;
    private bool invincible = false;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Start()
    {
        invincibleTimeWait = new WaitForSeconds(invincibleTime);
        meshRenderer = GetComponent<MeshRenderer>();
        enemy = GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            isEnemy = true;
        }
        else
        {
            player = GetComponent<PlayerBehaviour>();
            isPlayer = true;
        }
    }

    private void OnEnable()
    {
        health.SetValueToMax();
    }

    private static void Attack(CombatBehaviour target, AttackData attack, CombatBehaviour attacker)
    {
        target.TakeHit(attack, attacker.transform.position);
    }

    public void TakeHit(AttackData attack, Vector3 attackerPos)
    {
        if (invincible) return;
        health.UpdateValue(-attack.damage);

        if (isPlayer)
        {
            var force = transform.position - attackerPos;
            force = force.normalized * attack.knockback;
            player.AddForce(force);
        }
        
        StartCoroutine(nameof(Invincibility));
    }

    public void OnTriggerAttack(Collider other)
    {
        var target = other.GetComponent<CombatBehaviour>();
        
        if (target == null) return;
        if (target.faction == faction) return;
        
        Attack(target, triggerAttack, this);
    }
    
    private IEnumerator Invincibility()
    {
        if (isPlayer)
        {
            player.canMove = false;
        }
        invincible = true;
        meshRenderer.material.SetColor(EmissionColor,Color.red * Mathf.LinearToGammaSpace(10f));
        
        yield return invincibleTimeWait;
        
        meshRenderer.material.SetColor(EmissionColor,Color.black * Mathf.LinearToGammaSpace(10f));
        invincible = false;
        
        if (isPlayer)
        {
            player.canMove = true;
        }
    }
}
