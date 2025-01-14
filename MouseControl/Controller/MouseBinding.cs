using System;
using System.Diagnostics;

namespace MouseControl.Controller;
[Serializable]
public struct MouseBinding
{
	public MouseButtons Walk;
	public MouseButtons Jump;
	public MouseButtons Pause;
	public MouseButtons Confirm;
	public MouseButtons Cancel;
	public MouseButtons Boots;
	public MouseButtons Snake;
	public MouseButtons Restart;

	public MouseBinding() {
		Walk = MouseButtons.Right;
		Jump = MouseButtons.Left;
		Pause = MouseButtons.Middle;
		Confirm = MouseButtons.Left;
		Cancel = MouseButtons.Right;
		Snake = MouseButtons.X1;
		Boots = MouseButtons.X2;
		Restart = MouseButtons.None;
	}

	public MouseButtons Name2Button(string name) {
		switch (name) {
			case "Walk": return Walk;
			case "Jump": return Jump;
			case "Pause": return Pause;
			case "Confirm": return Confirm;
			case "Cancel": return Cancel;
			case "Boots": return Boots;
			case "Snake": return Snake;
			case "Restart": return Restart;
			default: return MouseButtons.None;
		}
	}
	public void SaveButton(string name, MouseButtons button) {
		Debug.WriteLine($"[DEBUG] Save '{name}' with {button}!");
		switch (name) {
			case "Walk":
				this.Walk = button;
				break;
			case "Jump":
				this.Jump = button;
				break;
			case "Pause":
				this.Pause = button;
				break;
			case "Confirm":
				this.Confirm = button;
				break;
			case "Cancel":
				this.Cancel = button;
				break;
			case "Boots":
				this.Boots = button;
				break;
			case "Snake":
				this.Snake = button;
				break;
			case "Restart":
				this.Restart = button;
				break;
		}
	}
}
