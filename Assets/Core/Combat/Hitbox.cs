using UnityEngine;

public class HitEvent {
  public HitConfig HitConfig;
  public Combatant Attacker;
  public Combatant Victim;
  /*
    public bool Blocked;
    public bool NoDamage {
      get {
        var wasBlocked = Blocked;
        var unkillable = !Victim.GetComponent<Killable>();
        var invulnerable = Victim.TryGetComponent(out Hearts hearts) && hearts.IsInvulnerable;
        return wasBlocked || unkillable || invulnerable;
      }
    }
    public float RecoilStrength {
      get {
        if (Attacker.TryGetComponent(out Knockbackable akb) && Victim.TryGetComponent(out Knockbackable vkb)) {
          var ascale = akb.RecoilScale(HitConfig.HitType);
          var vscale = vkb.RecoilScale(HitConfig.HitType);
          return Mathf.Min(ascale, vscale) * HitConfig.RecoilStrength;
        } else {
          return 0;
        }
      }
    }
    public float KnockbackStrength {
      get {
        if (Attacker.TryGetComponent(out Knockbackable akb) && Victim.TryGetComponent(out Knockbackable vkb)) {
          var ascale = akb.KnockbackScale(HitConfig.HitType);
          var vscale = vkb.KnockbackScale(HitConfig.HitType);
          return Mathf.Min(ascale, vscale) * HitConfig.KnockbackStrength;
        } else {
          return 0;
        }
      }
    }
  */
}

public class Hitbox : MonoBehaviour {
  public Combatant Owner;
  public AttributeValue Damage;
  [Tooltip("Only highest priority hitbox will register per attack")] public int Priority = 1;
  Collider Collider;

  public bool CollisionEnabled {
    get => Collider.enabled;
    set => Collider.enabled = value;
  }

  void Awake() {
    this.InitComponent(out Collider);
    Owner = Owner ?? GetComponentInParent<Combatant>();
  }

  void OnTriggerEnter(Collider c) {
    if (c.TryGetComponent(out Hurtbox hurtee))
      HitManager.Instance.OnHit(this, hurtee);
  }
}