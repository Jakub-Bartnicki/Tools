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

    private void OnSelect(MenuOption option)
    {
        switch (option)
        {
            case MenuOption.KeyE:
                Text = ItemRewriter.EditItem(Text);
                break;
            case MenuOption.KeyA:
                Text = ItemAdder.AddItem();
                break;
        }
    }
}
