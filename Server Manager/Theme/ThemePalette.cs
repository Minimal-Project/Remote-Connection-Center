using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Server_Manager.Theme;

public sealed record ThemePalette(
    Color Back,
    Color Fore,
    Color Surface,
    Color SurfaceAlt,
    Color Border,
    Color Accent,
    Color InputBack,
    Color InputFore
    )

{
    public static ThemePalette Light { get; } = new(
        Back: SystemColors.Control,
        Fore: SystemColors.ControlText,
        Surface: Color.White,
        SurfaceAlt: Color.FromArgb(245, 245, 245),
        Border: Color.FromArgb(210, 210, 210),
        Accent: Color.FromArgb(0, 120, 215),
        InputBack: Color.White,
        InputFore: SystemColors.ControlText
    );

    public static ThemePalette Dark { get; } = new(
        Back: Color.FromArgb(30, 30, 30),
        Fore: Color.FromArgb(220, 220, 220),
        Surface: Color.FromArgb(45, 45, 45),
        SurfaceAlt: Color.FromArgb(60, 60, 60),
        Border: Color.FromArgb(80, 80, 80),
        Accent: Color.FromArgb(0, 120, 215),
        InputBack: Color.FromArgb(45, 45, 45),
        InputFore: Color.FromArgb(220, 220, 220)
    );
}
