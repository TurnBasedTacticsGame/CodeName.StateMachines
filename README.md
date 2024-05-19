# UserInterface (Outdated)

This was the documentation for Codename's state machine before any of the library code was created.

## State Machines

The UI state machine is divided into 3 layers: states, capabilities, and the state machine itself.

### UiStates

UiStates are the traditional states in a finite state machine.

Only one state can be active at a time, but it is possible to change which state is active.

> Prefer to put mutually exclusive code here.

### UiCapabilities

UiCapabilities represent the features supported by each UiState. Capabilities were added to fix the code duplication between UiStates because some features can be used in multiple UiStates.

Multiple capabilities can be active and can be enabled/disabled at any time.
Each state typically enables its supported capabilities in OnEnable (in code) or through the UiStateDefaultCapabilities component (for in editor editing).

> Prefer to put code that represents features that can be enabled/disabled in multiple states here.

### UiContext / UiStateMachine

Because traditional state machines tend to have the issue of having all of the logic moved to the central state machine, even when it can be componentized, the UI state machine is split into two parts.

The UiContext is the class that tracks the current active states and capabilities, similar to a traditional state machine. However, this is its sole purpose. It does not define what transitions are available and when they happen.

The UiStateMachine exists not as a class, but as a concept. It is represented by any logic that is always active. For example, this allows global transition logic to be implemented.

To add always active functionality to the UiStateMachine, simply create a normal Unity MonoBehaviour and attach it to the UiContext. To improve organization, use child GameObjects as folders. See the [example setup](#example-setup) for a visual representation.

> Prefer to put global transition logic and other specialized logic that requires knowledge of all possible states here.

### Example setup

```
UiContext
- StateMachine
  - TransitionToReplayOnTurnFinished
- States
  - Waiting
  - ChooseGoal
  - ChooseAbility
  - Replay
- SharedCapabilities
  - Tooltip
  - Hover
```

In the ChooseGoal and ChooseAbility states, the player can hover and see tooltips. In the other two states, the player cannot.

The state machine is in charge of setting the state to Replay whenever a turn finishes.

> This setup is used in Codename Prototype's battle scenes, refer to it for more information if needed.

### Design notes

Everything in the UI state machine is exposed through Unity components.

For states and capabilities, this makes it very easy to tell what states and capabilities are active by simply looking in the Unity hierarchy.

Additionally, this allows the usual Unity event functions to be used.
