using SFML.Window;

namespace GGD_Template;

public class InputManager
{
    private static InputManager instance;

    private readonly Dictionary<Keyboard.Key, bool> isKeyDown = new();
    private readonly Dictionary<Keyboard.Key, bool> isKeyPressed = new();
    private readonly Dictionary<Keyboard.Key, bool> isKeyUp = new();

    // Simplified version of a Singelton 
    public static InputManager Instance
    {
        get { return instance ??= new InputManager(); }
    }

    private InputManager() { }

    public void Initialize(Window window)
    {
        window.SetKeyRepeatEnabled(false);
        window.KeyPressed += OnKeyPressed;
        window.KeyReleased += OnKeyReleased;

        InitKeyboardMap();
    }

    private void InitKeyboardMap()
    {
        isKeyDown.Add(Keyboard.Key.W, false);
        isKeyDown.Add(Keyboard.Key.A, false);
        isKeyDown.Add(Keyboard.Key.S, false);
        isKeyDown.Add(Keyboard.Key.D, false);
        isKeyDown.Add(Keyboard.Key.Space, false);
        isKeyDown.Add(Keyboard.Key.Num1, false);
        isKeyDown.Add(Keyboard.Key.Num2, false);
        isKeyDown.Add(Keyboard.Key.Escape, false);

        foreach (var key in isKeyDown.Keys)
        {
            isKeyUp.Add(key, false);
            isKeyPressed.Add(key, false);
        }
    }

    public void Update()
    {
        foreach (var kvp in isKeyDown)
            isKeyDown[kvp.Key] = false;

        foreach (var kvp in isKeyUp)
            isKeyUp[kvp.Key] = false;
    }

    public bool GetKeyPressed(Keyboard.Key key)
    {
        return isKeyPressed.ContainsKey(key) && isKeyPressed[key];
    }

    public bool GetKeyDown(Keyboard.Key key)
    {
        return isKeyDown.ContainsKey(key) && isKeyDown[key];
    }

    public bool GetKeyUp(Keyboard.Key key)
    {
        return isKeyUp.ContainsKey(key) && isKeyUp[key];
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