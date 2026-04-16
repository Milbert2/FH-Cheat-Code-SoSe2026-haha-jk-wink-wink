using SFML.Window;

namespace BGD;

public class InputManager
{
    private static InputManager instance;
    private readonly Dictionary<Keyboard.Key, bool> isKeyDown = new();
    private readonly Dictionary<Keyboard.Key, bool> isKeyPressed = new();
    private readonly Dictionary<Keyboard.Key, bool> isKeyUp = new();

    // Simplified version of a Singelton 
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

        isKeyUp.Add(Keyboard.Key.W, false);
        isKeyUp.Add(Keyboard.Key.A, false);
        isKeyUp.Add(Keyboard.Key.S, false);
        isKeyUp.Add(Keyboard.Key.D, false);
        isKeyUp.Add(Keyboard.Key.Space, false);
        isKeyUp.Add(Keyboard.Key.Num1, false);
        isKeyUp.Add(Keyboard.Key.Num2, false);

        isKeyPressed.Add(Keyboard.Key.W, false);
        isKeyPressed.Add(Keyboard.Key.A, false);
        isKeyPressed.Add(Keyboard.Key.S, false);
        isKeyPressed.Add(Keyboard.Key.D, false);
        isKeyPressed.Add(Keyboard.Key.Space, false);
        isKeyPressed.Add(Keyboard.Key.Num1, false);
        isKeyPressed.Add(Keyboard.Key.Num2, false);
    }

    public void update()
    {
        foreach (var Kvp in isKeyDown)
            isKeyDown[Kvp.Key] = false;

        foreach (var Kvp in isKeyUp)
            isKeyUp[Kvp.Key] = false;
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