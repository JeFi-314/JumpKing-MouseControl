using BehaviorTree;
using EntityComponent;
using EntityComponent.BT;
using HarmonyLib;
using IDrawable = JumpKing.Util.IDrawable;
using JumpKing;
using JumpKing.PauseMenu;
using JumpKing.PauseMenu.BT;
using JumpKing.PauseMenu.BT.Actions;
using JumpKing.PauseMenu.BT.Actions.BindController;
using JumpKing.Util;
using LanguageJK;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MouseControl.Controller;
using System.Windows.Forms;

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
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Walk)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Jump)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Pause)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Confirm)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Cancel)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Boots)));
        displayFrame.AddChild<DisplayMouseBind>(new DisplayMouseBind(nameof(binding.Snake)));
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
        menuSelector.AllowEscape = false;
        BindMouseDefault p_child2 = new BindMouseDefault();
        MenuSelectorBack p_child3 = new MenuSelectorBack(menuSelector);
        BTsequencor btsequencor = new BTsequencor();
        btsequencor.AddChild(p_child2);
        btsequencor.AddChild(p_child3);
        TimerAction timerAction = new TimerAction(language.MENUFACTORY_REVERTS_IN, 5, Color.Gray, btsequencor);
        menuSelector.AddChild<TextInfo>(new TextInfo(language.MENUFACTORY_KEEPCHANGES, Color.Gray));
        menuSelector.AddChild<TimerAction>(timerAction);
        menuSelector.AddChild<TextButton>(new TextButton(language.MENUFACTORY_NO, btsequencor));
        menuSelector.AddChild<TextButton>(new TextButton(language.MENUFACTORY_YES, p_child3));
        menuSelector.SetNodeForceRun(timerAction);
        menuSelector.Initialize(false);

        MenuFactoryDrawables.Add(menuSelector);

        BTsequencor btsequencor2 = new BTsequencor();
        btsequencor2.AddChild(new WaitUntilNoMenuInput());
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Walk), p_format));
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Jump), p_format));
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Pause), p_format));
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Confirm), p_format));
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Cancel), p_format));
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Boots), p_format));
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Snake), p_format));
        btsequencor2.AddChild(MakeBindButtonMenu(nameof(binding.Restart), p_format));
        btsequencor2.AddChild(new WaitUntilNoInputAll());
        btsequencor2.AddChild(menuSelector);

        BTselector btselector = new BTselector();
        btselector.AddChild(btsequencor2);
        btselector.AddChild(new PlaySFX(Game1.instance.contentManager.audio.menu.MenuFail));
        return btselector;
    }

    private static BindButtonFrame MakeBindButtonMenu(string p_button, GuiFormat p_format)
    {
        BTsequencor btsequencor = new BTsequencor();
        btsequencor.AddChild(new WaitUntilNoInputAll());
        btsequencor.AddChild(new BindMouseButton(p_button));
        BindButtonFrame bindButtonFrame = new BindButtonFrame(p_format, btsequencor);
        bindButtonFrame.AddChild<TextButton>(new TextButton(Util.ParseString("Press <string>", p_button), btsequencor));
        bindButtonFrame.Initialize();

        MenuFactoryDrawables.Add(bindButtonFrame);

        return bindButtonFrame;
    }
}