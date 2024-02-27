using System;
using System.Reflection;

[Serializable]
public class AbilityEventReference {
  public Ability Ability;
  public string EventName;

  public IEventSource GetEvent() {
    if (!Ability || EventName.Length == 0) return null;
    var fieldInfo = Ability.GetType().GetField(EventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    return (IEventSource)fieldInfo.GetValue(Ability);
  }
}
