using UnityEngine;

public class MoveAbility : Ability, IAbilityWithParameter<Vector2> {
  public Vector2 Parameter { get; set; }

  protected override void Awake() {
    base.Awake();
    RunEvent.Listen(() => Tick());
  }
  protected override void OnDestroy() {
    base.OnDestroy();
    RunEvent.Clear();
  }

  void Tick() {
    if (Parameter.sqrMagnitude > 0)
      Debug.Log($"Param = {Parameter}");
  }
}