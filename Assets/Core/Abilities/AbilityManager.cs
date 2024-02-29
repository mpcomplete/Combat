using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(ScriptExecutionGroups.Late)]
public class AbilityManager : MonoBehaviour {
  [HideInInspector, NonSerialized] public List<Ability> Abilities = new();

  IEnumerable<Ability> Cancellable => Abilities.Where(a => a.IsRunning && a.ActiveTags.HasAllFlags(AbilityTag.Cancellable | AbilityTag.OnlyOne));
  public void CancelAbilities() => Cancellable.ForEach(a => a.Stop());

  public void AddAbility(Ability ability) => Abilities.Add(ability);
  public void RemoveAbility(Ability ability) {
    ability.Stop();
    Abilities.Remove(ability);
  }

  public bool CanRun(Ability ability) {
    return ability.CanRun(ability.RunEvent);
  }

  public bool TryRun(Ability ability) {
    if (CanRun(ability)) {
      Run(ability);
      return true;
    }
    return false;
  }

  public bool TryRun<T>(Ability ability, T parameter) {
    if (CanRun(ability)) {
      Run(ability, parameter);
      return true;
    }
    return false;
  }

  public void Run(Ability ability) {
    CancelAbilities(); // TODO: only do if needed
    ability.RunEvent.Fire();
  }

  public void Run<T>(Ability ability, T parameter) {
    CancelAbilities();
    ((IAbilityWithParameter<T>)ability).Parameter = parameter;
    ability.RunEvent.Fire();
  }

  public void Stop(Ability ability) {
    if (ability.IsRunning)
      ability.Stop();
  }

  public TaskFunc RunUntilDone(Ability ability) => async s => {
    Run(ability);
    await s.Until(() => !ability.IsRunning);
  };

  void OnDestroy() => Abilities.ForEach(a => a.Stop());
}