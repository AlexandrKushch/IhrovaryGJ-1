using Godot;
using System;

[GlobalClass]
public partial class PrickableResource : Resource
{
    [Export]
    public PackedScene[] Items { get; set; }
}
