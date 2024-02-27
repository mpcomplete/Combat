using System.Threading.Tasks;
using UnityEngine;

// Test ability with multiple entry/exit points. SubAbility helps bind to these events.
public class CompositeAbility : TaskAbility {
  public EventSource ReleaseEvent = new();
  public EventSource AltEvent = new();

  public override async Task Run(TaskScope scope) {
    Debug.Log($"Pressed");
    await scope.Any(
      Waiter.ListenFor(ReleaseEvent),
      Waiter.Repeat(async s => {
        await s.ListenFor(AltEvent);
        Debug.Log($"Alt");
      }));
    Debug.Log($"Released");
  }
}