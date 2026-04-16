using SFML.Window;

namespace BGD;

public class InputManager
{
    private readonly Dictionary<Keyboard.Key, bool> isKeyDown = new();
    private readonly Dictionary<Keyboard.Key, bool> isKeyPressed = new();
    private readonly Dictionary<Keyboard.Key, bool> isKeyUp = new();

    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputManager();

            return instance;
        }
    }

    public void Initialize(Window window)
    {
        window.SetKeyRepeatEnabled(false);
        window.KeyPressed += OnKeyPressed;
        window.KeyReleased += OnKeyReleased;

        isKeyDown.Add(Keyboard.Key.W, false);
        isKeyDown.Add(Keyboard.Key.A, false);
        isKeyDown.Add(Keyboard.Key.S, false);
        isKeyDown.Add(Keyboard.Key.D, false);
        isKeyDown.Add(Keyboard.Key.Space, false);
        isKeyDown.Add(Keyboard.Key.Num1, false);
        isKeyDown.Add(Keyboard.Key.Num2, false);

        foreach (Keyboard.Key key in isKeyDown.Keys)
        {
            isKeyPressed[key] = false;
            isKeyUp[key] = false;
        }
    }

    public void Update()
    {
        foreach (Keyboard.Key key in isKeyDown.Keys)
            isKeyDown[key] = false;

        foreach (Keyboard.Key key in isKeyUp.Keys)
            isKeyUp[key] = false;
    }

    public bool GetKeyPressed(Keyboard.Key key)
    {
        return isKeyPressed.ContainsKey(key) ? isKeyPressed[key] : false;
    }

    public bool GetKeyDown(Keyboard.Key key)
    {
        return isKeyDown.ContainsKey(key) ? isKeyDown[key] : false;
    }

    public bool GetKeyUp(Keyboard.Key key)
    {
        return isKeyUp.ContainsKey(key) ? isKeyUp[key] : false;
    }

    private void OnKeyPressed(object sender, KeyEventArgs e)
    {
        if (isKeyPressed.ContainsKey(e.Code))
        {
            isKeyDown[e.Code] = true;
            isKeyPressed[e.Code] = true;
        }
    }

    private void OnKeyReleased(object sender, KeyEventArgs e)
    {
        if (isKeyPressed.ContainsKey(e.Code))
        {
            isKeyUp[e.Code] = true;
            isKeyPressed[e.Code] = false;
        }
    }
}