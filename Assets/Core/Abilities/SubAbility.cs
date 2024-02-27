public class SubAbility : Ability {
  public AbilityEventReference Event;

  Ability Ability;
  IEventSource AbilityEvent;

  public override AbilityTag ActiveTags => Tags | Ability.ActiveTags;

  // TODO arg
  public override bool CanRun() => Ability.CanRun();

  protected override void Awake() {
    base.Awake();
    Ability = Event.Ability;
    AbilityEvent = Event.GetEvent();
    RunEvent.Listen(() => AbilityEvent.Fire());
  }
  protected override void OnDestroy() {
    base.OnDestroy();
    RunEvent.Clear();
  }
}