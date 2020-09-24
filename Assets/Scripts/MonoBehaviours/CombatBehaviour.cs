using System.Collections;
using UnityEngine;

public class CombatBehaviour : MonoBehaviour
{
    public enum FactionEnum {Good,Bad,Ugly}

    public FactionEnum faction;
    public HealthData health;
    [SerializeField] private AttackData triggerAttack;

    private MeshRenderer meshRenderer;

    [SerializeField] private float invincibleTime = 1f;
    private bool invincible = false;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        health.Initialize();
    }

    private static void Attack(CombatBehaviour target, AttackData attack)
    {
        target.TakeHit(attack);
    }

    public void TakeHit(AttackData attack)
    {
        if (invincible) return;
        health.AffectHealth(-attack.damage);
        StartCoroutine(nameof(Invincibility));
    }

    public void OnTriggerAttack(Collider other)
    {
        var target = other.GetComponent<CombatBehaviour>();
        
        if (target == null) return;
        if (target.faction == faction) return;
        
        Attack(target, triggerAttack);
    }
    
    private IEnumerator Invincibility()
    {
        invincible = true;
        meshRenderer.material.SetColor(EmissionColor,Color.red * Mathf.LinearToGammaSpace(10f));
        
        yield return new WaitForSeconds(invincibleTime);
        
        meshRenderer.material.SetColor(EmissionColor,Color.black * Mathf.LinearToGammaSpace(10f));
        invincible = false;
    }
}
