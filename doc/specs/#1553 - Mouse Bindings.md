author: Tom Sillence @ts252
created on: 2020-01-30
last updated: 2020-01-30
issue id: 1553
---

# Mouse bindings

## Abstract

The concept of keybindings will be extended to let the user assign commands to combinations of mouse buttons as well as key presses. Whilst the scope of possible mouse interactions with the terminal UI is vast, this spec covers:
  - only mouse-down events 
  - only events within the terminal window (not the tab bar or elsewhere)
  - only commands that currently exist as keybindings
  

## Inspiration

Mouse interaction is currently not configurable. Some users (including the author) want to be able to assign the middle mouse button to "paste", following x-windows convention. Many like the current behaviour and others naturally have divergent ideas. We want a settings schema that can express the popularly-requested options and also pave the way for future mouse behaviours that are beyond the scope of this spec.

## Solution Design

  ***This is not the only way to structure the settings.*** *In the initial proposal, commands were assigned to mouse events (expressed as `(device,event,where)`) under a separate key. I want to support "chords" of both keys and mouse buttons (imagine ctrl+scrollup for pageup, or shift-click for paste) and the more I looked at it, the more inviting the existing `keys` arrays seemed.*

  - names for the various mouse buttons and touch/stylus interactions will be defined, e.g. "leftclick", "middleclick", "wheelup".
  - in `profiles.json` these will be allowed to appear as part of the key-chord strings in the `globals.keybindings[n].keys` arrays.
  - a new command: `null` will be introduced to allow the suppression of default behaviour
  - the `KeyChord` class will be extended to accommodate these values
  - the point of departure for handing mouse events will be XXXXX, where right-clicks are currently handled. When there are pointer events, the key chord state will be evaluated and commands dispatched.
  - the current mouse-based scroll, copying and pasting (but not selection) behaviour will be re-expressed as default "keys" entries for the corresponding commands
  - if ctrl-mouseleft and 
  


## UI/UX Design

UI changes are strictly non-visual because the configuration is via profiles.json and only existing commands will be implemented at first. The ability to use the mouse for more than just selection, scrolling and copy/paste will be a striking change for those who choose to use it however.

## Capabilities

### Accessibility

Provided the user controls their own `profiles.json` this change will never make terminal *less* accessible. Some users who find it hard to hold down multiple keys at once may find it more convenient to assign certain commands their pointing device.

### Security

No imapact expected.

### Reliability

This is not reliability-enhancing. It adds an amount of complexity, hopefully justified by the happy users enjoying some new functionality.

### Compatibility

As a priority, the current mouse behaviour will be maintained as the default. Some behaviours such as scrolling on mousewheel will be brought within the scope of these settings. At the UX level, this change enhances compatibilty with other terminals by allowing users to emulate more of their familiar environment.

### Performance, Power, and Efficiency

Additional processing is trivial will only take place when there are pointer events. There should be no measurable performance impact. Since we are not interested in any mousemove events (or equivalent), the worst that can happen is a flood of wheel events, which will be no worse than keyboard repeat on an existing keyboard command.

## Potential Issues

  - It will be possible to specify very silly behaviour e.g. mapping left click to close tab. This is mitigated by the fact that only the terminal part of the window is affected, so it should always be possible to go back and fix mistakes.

## Future considerations

The following features have been mentioned and won't be immediately satisfied by this spec:
  - **context menu:** this should be expressible as a new command mapped to right-click and/or a key combination.
  - **configurable scroll amount:** you could introduce `keybindings[n].parameter` and have the ability to pass a value to a command
  - **passing mouse events to the VT:** I suggest a new command "passToVT", similar to the "null" command introduced above. There is room to argue about defaults but you could imagine passToVT having a default keys of ["leftmouse", "button4", "button5"]. This would allow users to assign a command to a button and **also** pass it to the VT, which could be handy.
  - **other mouse events (e.g. double-click)**: this is a challenge to the settings structure. 
  - **touch and non-mouse pointing devices**: I think these can be divided up. Instantaneous, one-shot events e.g. long-touch are ripe for inclusion.

## Resources
