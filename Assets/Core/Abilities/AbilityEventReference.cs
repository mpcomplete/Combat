using System;
using System.Reflection;

[Serializable]
public class AbilityEventReference {
  public Ability Ability;
  public string EventName;

  public IEventSource GetEvent() {
    if (!Ability || EventName.Length == 0) return null;
    var propertyInfo = Ability.GetType().GetProperty(EventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    return (IEventSource)propertyInfo.GetValue(Ability);
  }
}
