using TodoTool.Enums;

namespace TodoTool.Menu;

internal sealed class Option
{
    internal string Text { get; set; }
    internal Action<MenuOption> Selected { get; }

    internal Option(string name)
    {
        Text = name;
        Selected = OnSelect;
    }

    private void OnSelect(MenuOption option = MenuOption.KeyU)
    {
        switch (option)
        {
            case MenuOption.Enter:
                break;
            case MenuOption.KeyU:
                Text = ItemRewriter.UpdateItem(Text);
                break;
            case MenuOption.KeyA:
                Text = ItemAdder.AddLine();
                break;
            case MenuOption.KeyR:
                break;
            default:
                break;
        }
    }
}
