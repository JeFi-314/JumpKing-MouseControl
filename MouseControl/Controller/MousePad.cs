using Microsoft.Xna.Framework.Input;
using JumpKing.Controller;
using JumpKing;
using Steamworks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MouseControl.Controller;

public static class MousePad
{
	public static MouseBinding Binding;
	private static bool[] pressed;
	private static PadState currentState;
	private static PadState lastState;
	private static int lastWheelValue;
	private static bool steam_overlay_active;

	public static bool[] PressedButtons
	{
		get => pressed;
		set => pressed = value;
	}

	static MousePad() {
		RegisterSteamCallback();
		lastWheelValue = Mouse.GetState().ScrollWheelValue;
		pressed = new bool[6];
		currentState = lastState = default;
		Binding = new MouseBinding();
	}
	public static string ButtonToString(MouseButtons p_button)
	{
		return p_button.ToString();
	}

	private static PadState GetPadState()
	{
		if (steam_overlay_active || !Game1.instance.IsActive)
		{
			return default;
		}
		MouseState mouse = Mouse.GetState();

		// It is possible that click left & right at same time will stuck both buttons
		// looks like xna bug or device issue
		pressed[0] = false;
		pressed[1] = mouse.LeftButton == ButtonState.Pressed;
		pressed[2] = mouse.MiddleButton == ButtonState.Pressed;
		pressed[3] = mouse.RightButton == ButtonState.Pressed;
		pressed[4] = mouse.XButton1 == ButtonState.Pressed;
		pressed[5] = mouse.XButton2 == ButtonState.Pressed;

		PadState result = default;
		if (mouse.ScrollWheelValue>lastWheelValue) result.up = true;
		else if (mouse.ScrollWheelValue<lastWheelValue) result.down = true;
		lastWheelValue = mouse.ScrollWheelValue;
		if (pressed[(int)Binding.Jump]) result.jump = true;
		if (pressed[(int)Binding.Pause]) result.pause = true;
		if (pressed[(int)Binding.Confirm]) result.confirm = true;
		if (pressed[(int)Binding.Cancel]) result.cancel = true;
		if (pressed[(int)Binding.Boots]) result.boots = true;
		if (pressed[(int)Binding.Snake]) result.snake = true;
		if (pressed[(int)Binding.Restart]) result.restart = true;

		Rectangle gameRect = Game1.instance.GetGameRect();
		int leftBound = gameRect.X+(int)(gameRect.Width*MouseControl.Prefs.SideRatio/2);
		int rightBound = gameRect.X+(int)(gameRect.Width*(1-MouseControl.Prefs.SideRatio/2));
		if (MouseControl.Prefs.isRLControl) {
			if (result.jump) {
				if (mouse.X<=leftBound) {
					result.left = true;
					CursorManager.SetCursor("LeftJump");
				} else if (mouse.X<=rightBound) {
					CursorManager.SetCursor("NormalJump");
				} else {
					result.right = true;
					CursorManager.SetCursor("RightJump");
				}
			} else if (pressed[(int)Binding.Walk]) {
				if (mouse.X<=leftBound) {
					result.left = true;
					CursorManager.SetCursor("LeftWalk");
				} else if (mouse.X<=rightBound) {
					CursorManager.SetCursor("Normal");
				} else {
					result.right = true;
					CursorManager.SetCursor("RightWalk");
				}
			} else {
				if (mouse.X<=leftBound) {
					CursorManager.SetCursor("Left");
				} else if (mouse.X<=rightBound) {
					CursorManager.SetCursor("Normal");
				} else {
					CursorManager.SetCursor("Right");
				}
			}
		} else {
			if (result.jump) CursorManager.SetCursor("NormalJump");
			else CursorManager.SetCursor("Normal");
		}

		return result;
	}

	public static void Update()
	{
		lastState = currentState;
		currentState = GetPadState();
	}
	public static PadState GetState()
	{
		return currentState;
	}
	public static PadState GetPressed()
	{
		PadState result = new PadState();
		result.up = !lastState.up && currentState.up;
		result.down = !lastState.down && currentState.down;
		result.left = !lastState.left && currentState.left;
		result.right = !lastState.right && currentState.right;
		result.jump = !lastState.jump && currentState.jump;
		result.pause = !lastState.pause && currentState.pause;
		result.confirm = !lastState.confirm && currentState.confirm;
		result.cancel = !lastState.cancel && currentState.cancel;
		result.boots = !lastState.boots && currentState.boots;
		result.snake = !lastState.snake && currentState.snake;
		result.restart = !lastState.restart && currentState.restart;
		return result;
	}
	public static void RegisterSteamCallback()
	{
		_ = Callback<GameOverlayActivated_t>.Create(OnOverlayActivated);
	}
	private static void OnOverlayActivated(GameOverlayActivated_t p_info)
	{
		steam_overlay_active = p_info.m_bActive != 0;
	}
}
