namespace Server_Manager.Theme;

/// <summary>
///  A control that paints itself and its own children. <see cref="ThemeManager"/> hands such a
///  control the job instead of applying the generic per-control rules, which would otherwise
///  overwrite colors the control derives itself - a server card's status text, for instance.
/// </summary>
public interface IThemedControl
{
    void ApplyTheme();
}
