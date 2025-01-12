using System.ComponentModel;
using System.Runtime.CompilerServices;
using JumpKing;
using Microsoft.Xna.Framework.Input;

namespace MouseControl;
public class Preferences : INotifyPropertyChanged
{
    private bool _enable = false;
    public bool isEnable
    {
        get => _enable;
        set
        {
            Game1.instance.IsMouseVisible = _enable = value;
            OnPropertyChanged();
        }
    }
    private bool _controlDirection = false;
    public bool isControlDirection
    {
        get => _controlDirection;
        set
        {
            if (!value) Mouse.SetCursor(MouseCursor.Crosshair);
            _controlDirection = value;
            OnPropertyChanged();
        }
    }

    private float _sideRatio = 0;
    public float SideRatio
    {
        get => _sideRatio;
        set
        {
            _sideRatio = value;
            OnPropertyChanged();
        }
    }

    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
    }