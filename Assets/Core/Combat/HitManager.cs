using System.Collections.Generic;
using UnityEngine;

// TODO: More sophisticated. Ignore dupe hits on subsequent frames.
public class HitManager : SingletonBehavior<HitManager> {
  List<(Hitbox, Hurtbox)> Hits;

  public void OnHit(Hitbox attacker, Hurtbox victim) {
    if (Hits.Find(h => h.Item1 == attacker && h.Item2 == victim) is var h && h.Item1) {
      if (h.Item1.Priority > attacker.Priority)
        return;
      Hits.Remove(h);
    }
    Hits.Add((attacker, victim));
  }

  void ProcessHit(Hitbox attacker, Hurtbox victim) {
    Debug.Log($"{attacker.Owner} hit {victim.Owner}");
  }

  void FixedUpdate() {
    Hits.ForEach(h => ProcessHit(h.Item1, h.Item2));
    Hits.Clear();
  }

}