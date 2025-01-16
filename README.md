# Mouse King - A Mouse Controls Mod for Jump King

This mod lets you control Jump King with a mouse, offering options for input bindings, cursor icons, and movement settings.

You can find it on [Steam Workshop Page](https://steamcommunity.com/sharedfiles/filedetails/?id=3407882062).

---

## Basic Movement

As with other device controls for Jump King, you can use mouse buttons to perform every input except for four-directional controls. The Up/Down actions are always bound to the middle button scroll-up/down, while Left/Right can be triggered by pressing the jump or walk buttons on the corresponding side of the screen (if R/L control is enabled). For normal gameplay, you can:

- Press the walk button to make the player walk left or right (as Left/Right normal input).
- Press the jump button to charge a jump, then jump left, neutral, or right (as Left/None/Right + Jump input).
- Disable R/L control to use the mouse as a no-direction input device.

---

## Menu Features

### Controls
- **Bind Buttons**: Set game inputs by pressing a physical button during the countdown. Use the middle button scroll-up/down or other device inputs to skip key binds, leaving them empty (`-`). Five physical buttons are available: left, middle, right click, and X1/X2 (side buttons).
- **Default**: Reset bindings to default: Jump (left), Confirm (left), Walk (right), Cancel (right), Pause (middle), Snake (X1), Boots (X2), Restart (none).
- **Save**: Save bindings to a file for future use.

### Additional Options
- **Show Cursor**: Show or hide the cursor icon.
- **Bound Cursor**: Restrict the cursor to the game window (Alt+Tab to switch windows if enabled).
- **R/L Control**: Enable to trigger Left/Right inputs by pressing jump or walk buttons in the side area. Cursor icons update to indicate active R/L control.
- **Side Area**: Adjust the percentage of side areas used for Left/Right control. The black areas on both sides always count as side areas.
- **Resize**: Resize custom cursors from x1 to x10.

---

## Cursor Icons

Cursor icons change depending on your cursor position, settings, and input status. Below are the icon names corresponding to each situation:

- **"Left"/"Normal"/"Right"**: When no input is pressed.
- **"LeftWalk"/"RightWalk"**: When Walk is pressed but not Jump.
- **"LeftJump"/"NormalJump"/"RightJump"**: When Jump is pressed.

If R/L control is disabled, Left and Right icons will be replaced by Normal (e.g., "NormalWalk" = "Normal").

### Customizing Cursor Icons
You can customize your cursor icons by replacing the `icons/<icon-name>.xnb` files in the workshop folder. The default location is:
```
C:\Program Files (x86)\Steam\steamapps\workshop\content\1061090\3407882062
```
(Note: The location may vary depending on where you installed Steam.)

To create a custom icon:
1. Ensure your image size is **N x N** (a square shape).
2. The middle point of the image will act as the cursor hotspot (e.g., the head point of a normal arrow cursor). If the pixel size is an even number (2N), the hotspot will be the bottom-right pixel near the center.

Once your icon is ready, use the tools provided in the [workshop documentation](https://teamnexile.github.io/jk-workshop-docs/tools/alternatives/) to pack your image into `.xnb` format.

---

Enjoy using the mouse controls mod!

