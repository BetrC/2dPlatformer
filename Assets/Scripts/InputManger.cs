using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InputManger : Singleton<InputManger>
{
    private PlayerInput _input;

    public PlayerInput Input
    {
        get
        {
            if (_input == null)
                _input = new PlayerInput();
            return _input;
        }
        set
        {
            _input = value;
        }
    }
}
