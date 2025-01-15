using BehaviorTree;
using HarmonyLib;
using IDrawable = JumpKing.Util.IDrawable;
using JumpKing;
using JumpKing.PauseMenu;
using JumpKing.PauseMenu.BT;
using JumpKing.PauseMenu.BT.Actions;
using JumpKing.PauseMenu.BT.Actions.BindController;
using JumpKing.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MouseControl.Controller;
using System.Collections.Generic;

namespace MouseControl.Nodes;
public class MenuBindControls : BTsequencor
{
    public static List<IDrawable> MenuFactoryDrawables;
    private static MouseBinding binding = new MouseBinding();
    public MenuBindControls(object menuFactory) : base()
    {
        BTsimultaneous bTsimultaneous = new BTsimultaneous();
        var factory = Traverse.Create(menuFactory);
        var gui_left = factory.Field("CONTROLS_GUI_FORMAT_LEFT").GetValue<GuiFormat>();
        var gui_right = factory.Field("CONTROLS_GUI_FORMAT_RIGHT").GetValue<GuiFormat>();

        MenuFactoryDrawables = factory.Field("m_drawables").GetValue<List<IDrawable>>();

        MenuSelector menuSelector = new MenuSelector(gui_left);

        bTsimultaneous.AddChild(menuSelector);
        MenuFactoryDrawables.Add(menuSelector);

        // left
        SpriteFont menuFontSmall = Game1.instance.contentManager.font.MenuFontSmall;
        int frameIndex = MenuFactoryDrawables.Count;
        menuSelector.AddChild<TextButton>(new TextButton("Bind buttons", MakeBindMouse(), menuFontSmall));
        menuSelector.AddChild<TextButton>(new TextButton("Default", new BindMouseDefault(), menuFontSmall));
        menuSelector.AddChild<SaveTextButton>(new SaveTextButton(new SaveMouseBind(), menuFontSmall));
        menuSelector.Initialize(true);
        menuSelector.GetBounds();

        // right
        DisplayFrame displayFrame = new DisplayFrame(gui_right, BTresult.Running);
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Jump)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Confirm)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Walk)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Cancel)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Pause)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Snake)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Boots)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Restart)));
        displayFrame.Initialize();

        MenuFactoryDrawables.Insert(frameIndex, displayFrame);
        
        bTsimultaneous.AddChild(new StaticNode(displayFrame, BTresult.Failure));
        this.AddChild(new CheckEnable());
        this.AddChild(bTsimultaneous);
    }
    private static IBTnode MakeBindMouse()
    {
        GuiFormat p_format = new GuiFormat
        {
            anchor_bounds = new Rectangle(0, 0, 480, 360),
            anchor = new Vector2(1f, 1f) / 2f,
            all_margin = 16,
            element_margin = 8,
            all_padding = 16
        };

        MenuSelector menuSelector = new MenuSelector(p_format);
        MenuFactoryDrawables.Add(menuSelector);

        BTsequencor btsequencor = new BTsequencor();
        btsequencor.AddChild(new BTevaluator(new WaitUntilNoInputAll(), new WaitUntilNoInputMouse()));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Jump), p_format));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Confirm), p_format));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Walk), p_format));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Cancel), p_format));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Pause), p_format));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Snake), p_format));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Boots), p_format));
        btsequencor.AddChild(MakeBindButtonMenu(nameof(binding.Restart), p_format));
        btsequencor.AddChild(new BTevaluator(new WaitUntilNoInputAll(), new WaitUntilNoInputMouse()));
        btsequencor.AddChild(menuSelector);

        menuSelector.AllowEscape = false;
        BindMouseDefault bindMouseDefault = new BindMouseDefault();
        MenuSelectorBack menuSelectorBack = new MenuSelectorBack(menuSelector);
        BTsequencor btsequencor2 = new BTsequencor();
        btsequencor2.AddChild(bindMouseDefault);
        btsequencor2.AddChild(menuSelectorBack);
        menuSelector.AddChild<TextInfo>(new TextInfo("Keep changes?", Color.Gray));
        TimerAction timerAction = new TimerAction("Resets in <number>", 5, Color.Gray, btsequencor2);
        menuSelector.AddChild<TimerAction>(timerAction);
        menuSelector.AddChild<TextButton>(new TextButton("No", btsequencor2));
        menuSelector.AddChild<TextButton>(new TextButton("Yes", menuSelectorBack));
        menuSelector.SetNodeForceRun(timerAction);
        menuSelector.Initialize(false);

        BTselector btselector = new BTselector();
        btselector.AddChild(btsequencor);
        btselector.AddChild(new PlaySFX(Game1.instance.contentManager.audio.menu.MenuFail));
        return btselector;
    }

    private static BindButtonFrame MakeBindButtonMenu(string p_button, GuiFormat p_format)
    {
        BTsequencor btsequencor = new BTsequencor();
        btsequencor.AddChild(new BTevaluator(new WaitUntilNoInputAll(), new WaitUntilNoInputMouse()));
        btsequencor.AddChild(new BindMouseButton(p_button));
        BindButtonFrame bindButtonFrame = new BindButtonFrame(p_format, btsequencor);
        bindButtonFrame.AddChild<TextButton>(new TextButton($"Press {p_button}", btsequencor));
        bindButtonFrame.Initialize();

        MenuFactoryDrawables.Add(bindButtonFrame);

        return bindButtonFrame;
    }
}