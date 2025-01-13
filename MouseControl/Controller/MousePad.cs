using Microsoft.Xna.Framework.Input;
using JumpKing.Controller;
using JumpKing;
using Steamworks;
using Microsoft.Xna.Framework;

namespace MouseControl.Controller;

public static class MousePad
{
	private static Binding binding;
	private static PadState currentState;
	private static PadState lastState;
	private static bool[] pressed;
	private static int lastWheelValue;
	private static bool steam_overlay_active;

	static MousePad() {
		RegisterSteamCallback();
		lastWheelValue = Mouse.GetState().ScrollWheelValue;
		pressed = new bool[6];
		currentState = lastState = default;
		binding = GetDefaultBind();
	}
	public static string ButtonToString(MouseButtons p_button)
	{
		return p_button.ToString();
	}
	public static Binding GetDefaultBind()
	{
		return new Binding {
			walk = MouseButtons.RightButton,
			jump = MouseButtons.LeftButton,
			pause = MouseButtons.MiddleButton,
			confirm = MouseButtons.LeftButton,
			cancel = MouseButtons.RightButton,
			snake = MouseButtons.XButton1,
			boots = MouseButtons.XButton2,
			restart = MouseButtons.None,
		};
	}
	private static PadState GetPadState()
	{
		if (steam_overlay_active || !Game1.instance.IsActive)
		{
			return default;
		}
		MouseState mouse = Mouse.GetState();

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
		if (pressed[(int)binding.jump]) result.jump = true;
		if (pressed[(int)binding.pause]) result.pause = true;
		if (pressed[(int)binding.confirm]) result.confirm = true;
		if (pressed[(int)binding.cancel]) result.cancel = true;
		if (pressed[(int)binding.boots]) result.boots = true;
		if (pressed[(int)binding.snake]) result.snake = true;
		if (pressed[(int)binding.restart]) result.restart = true;

		Rectangle gameRect = Game1.instance.GetGameRect();
		int leftBound = gameRect.X+(int)(gameRect.Width*ModEntry.Prefs.SideRatio/2);
		int rightBound = gameRect.X+(int)(gameRect.Width*(1-ModEntry.Prefs.SideRatio/2));
		if (ModEntry.Prefs.isRLControl) {
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
			} else if (pressed[(int)binding.walk]) {
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
