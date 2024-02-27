using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour {
  [SerializeField] AbilityManager AbilityManager;
  //[SerializeField] Ability Move;
  [SerializeField] Ability TestAction;
  [SerializeField] Ability TestAlt;
  [SerializeField] Ability TestRelease;

  Inputs Inputs;

  void Awake() {
    Inputs = new();
    Inputs.Enable();
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

    if (Inputs.Player.Main.WasPressedThisFrame())
      AbilityManager.TryRun(TestAction);
    if (Inputs.Player.Alt.WasPressedThisFrame())
      AbilityManager.TryRun(TestAlt);
    if (Inputs.Player.Main.WasReleasedThisFrame())
      AbilityManager.TryRun(TestRelease);
  }
}