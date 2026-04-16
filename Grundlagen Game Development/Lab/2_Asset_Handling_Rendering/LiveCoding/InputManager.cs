using SFML.Graphics;
using SFML.Window;

public class InputManager
{
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

    private Dictionary<Keyboard.Key, bool> keysPressed = new();
    private Dictionary<Keyboard.Key, bool> keysPressedThisFrame = new();

    private InputManager() { }

    public void Initialize(RenderWindow window)
    {
        window.SetKeyRepeatEnabled(false);

        keysPressed[Keyboard.Key.A] = false;
        keysPressed[Keyboard.Key.W] = false;
        keysPressed[Keyboard.Key.S] = false;
        keysPressed[Keyboard.Key.D] = false;
        keysPressed[Keyboard.Key.Space] = false;

        foreach (var key in keysPressed.Keys)
            keysPressedThisFrame[key] = false;

        window.KeyPressed += OnKeyPressed;
        window.KeyReleased += OnKeyReleased;
    }

    private void OnKeyReleased(object sender, KeyEventArgs e)
    {
        if (keysPressed.ContainsKey(e.Code))
            keysPressed[e.Code] = false;
    }

    private void OnKeyPressed(object sender, KeyEventArgs e)
    {
        if (keysPressed.ContainsKey(e.Code))
        {
            keysPressed[e.Code] = true;
            keysPressedThisFrame[e.Code] = true;
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var key in keysPressedThisFrame.Keys)
        {
            keysPressedThisFrame[key] = false;
        }
    }

    public bool GetKeyPressed(Keyboard.Key key)
    {
        return keysPressed.ContainsKey(key) ? keysPressed[key] : false;
    }

    public bool GetKeyDown(Keyboard.Key key)
    {
        return keysPressedThisFrame.ContainsKey(key) ? keysPressedThisFrame[key] : false;
    }
}