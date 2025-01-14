using BehaviorTree;
using EntityComponent;
using EntityComponent.BT;
using JumpKing;
using JumpKing.PauseMenu;
using JumpKing.PauseMenu.BT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MouseControl.Controller;

namespace MouseControl.Nodes;
public class DisplayMouseBind : IBTnode, IMenuItem, UnSelectable
{
    private string bindName;
    private SpriteFont Font => Game1.instance.contentManager.font.MenuFontSmall;

    public DisplayMouseBind(string p_bindName) {
        bindName = p_bindName;
    }

    public void Draw(int x, int y, bool selected)
    {
        string str = bindName + " : ";
        MenuItemHelper.Draw(x, y, str, Color.Gray, Font);

        x += (int)(GetSize().X / 2f);
        str = MousePad.Binding.Name2Button(bindName).ToString();
        if (str == "None") str = "-";
        str = FormatString(str);
        MenuItemHelper.Draw(x, y, str, Color.Gray, Font);
    }

    private string FormatString(string p_string)
    {
        int num = GetSize().X / 2;
        if (MenuItemHelper.GetSize(p_string, Font).X > num)
        {
            while (MenuItemHelper.GetSize(p_string, Font).X > num)
            {
                p_string = p_string.Substring(0, p_string.Length - 1);
            }

            return p_string.Substring(0, p_string.Length - 1) + "*";
        }

        return p_string;
    }

    public Point GetSize()
    {
        return MenuItemHelper.GetSize("xbox 360 controller 1____         ", Font);
    }

    protected override BTresult MyRun(TickData p_data)
    {
        return BTresult.Failure;
    }
}
