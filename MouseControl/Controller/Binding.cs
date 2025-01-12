using System;
using JumpKing.Controller;
using JumpKing.SaveThread;

namespace MouseControl.Controller;

[Serializable]
public struct Binding : ISaveable<Binding>
{
	public MouseButtons walk;
	public MouseButtons jump;
	public MouseButtons pause;
	public MouseButtons confirm;
	public MouseButtons cancel;
	public MouseButtons boots;
	public MouseButtons snake;
	public MouseButtons restart;

	public Binding GetDefault() {
		return new Binding();
	}

}
