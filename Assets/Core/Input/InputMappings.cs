using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputActionType {
  JustDown,
  JustUp,
  Always,  // Triggers every frame, most useful with a parameter (e.g. Move with the Vector2 axis value).
}

[Serializable]
public struct InputToAbilityMapping {
  public InputActionReference Action;
  public InputActionType Type;
  public Ability Ability;
}

// Maps InputActions to Abilities.
[DefaultExecutionOrder(-1)]
public class InputMappings : MonoBehaviour {
  [SerializeField] AbilityManager AbilityManager;
  [SerializeField] InputToAbilityMapping Move;
  [SerializeField] List<InputToAbilityMapping> Mappings;

  Inputs Inputs;

  void Awake() {
    Inputs = new();
    Inputs.Enable();
    Move.Action.asset.Enable();
    Mappings.ForEach(m => m.Action.asset.Enable());
  }

  void OnDestroy() {
    Inputs.Disable();
    Inputs.Dispose();
  }

  void FixedUpdate() {
    AbilityManager.TryRun(Move.Ability, Move.Action.action.ReadValue<Vector2>());

    // TODO: This is just a skeleton. Features to add/consider:
    // - buffering
    Mappings.ForEach(m => {
      m.Action.asset.Enable();
      switch (m.Type) {
      case InputActionType.JustDown: if (m.Action.action.WasPerformedThisFrame()) AbilityManager.TryRun(m.Ability); break;
      case InputActionType.JustUp: if (m.Action.action.WasReleasedThisFrame()) AbilityManager.TryRun(m.Ability); break;
      }
    });
  }
}