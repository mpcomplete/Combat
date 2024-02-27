using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputActionType {
  JustDown,
  JustUp,
  Always,
}

[Serializable]
public struct InputToAbilityMap {
  public InputActionReference Action;
  public InputActionType Type;
  public Ability Ability;
}

[DefaultExecutionOrder(-1)]
public class InputMappings : MonoBehaviour {
  [SerializeField] AbilityManager AbilityManager;
  //[SerializeField] Ability Move;
  [SerializeField] List<InputToAbilityMap> Mappings;

  Inputs Inputs;

  void Awake() {
    Inputs = new();
    Inputs.Enable();
    Mappings.ForEach(m => m.Action.asset.Enable());
  }

  void OnDestroy() {
    Inputs.Disable();
    Inputs.Dispose();
  }

  void FixedUpdate() {
    //var move = Inputs.Player.Move.ReadValue<Vector2>();
    //if (AbilityManager.CanRun(Move)) {
    //  // TODO: move val
    //  AbilityManager.Run(Move);
    //}

    // TODO: This is just a skeleton. Features to add/consider:
    // - buffering
    // - parameters (for axis mappings, action.ReadValue<Vector2> piped to Ability in a generic way)
    Mappings.ForEach(m => {
      m.Action.asset.Enable();
      switch (m.Type) {
      case InputActionType.JustDown: if (m.Action.action.WasPerformedThisFrame()) AbilityManager.TryRun(m.Ability); break;
      case InputActionType.JustUp: if (m.Action.action.WasReleasedThisFrame()) AbilityManager.TryRun(m.Ability); break;
      case InputActionType.Always: AbilityManager.TryRun(m.Ability); break;
      }
    });
  }
}