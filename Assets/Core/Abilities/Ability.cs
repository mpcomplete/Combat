using System.Threading.Tasks;
using UnityEngine;

public delegate Task AbilityMethod(TaskScope scope);

[DefaultExecutionOrder(ScriptExecutionGroups.Ability)]
public abstract class Ability : MonoBehaviour {
  // Main entry point. Use SubAbility to bind an ability to an event source other than Main.
  public EventSource RunEvent;
  public AbilityTag TagsWhenActive;

  public virtual AbilityTag ActiveTags => Tags;
  public virtual bool IsRunning => false;
  public virtual bool CanRun() => true;
  public virtual void Stop() { Tags = default;  }

  protected AbilityManager AbilityManager;
  protected AbilityTag Tags;

  protected virtual void Awake() {
    AbilityManager = GetComponentInParent<AbilityManager>();
    if (AbilityManager)  // Ability may be detached, e.g. part of an item
      AbilityManager.AddAbility(this);
  }

  protected virtual void OnDestroy() {
    if (AbilityManager)
      AbilityManager.RemoveAbility(this);
  }
}