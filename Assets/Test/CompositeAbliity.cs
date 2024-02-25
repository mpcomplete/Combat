using System.Threading.Tasks;
using UnityEngine;

public class CompositeAbility : TaskAbility {
  public EventSource ReleaseEvent;
  public EventSource SwitchEvent;

  public override async Task Run(TaskScope scope) {
    await scope.Any(
      Waiter.ListenFor(ReleaseEvent),
      Waiter.Repeat(async s => {
        await s.ListenFor(SwitchEvent);
        Debug.Log($"Switch");
      }));
    Debug.Log($"Release");
  }
}