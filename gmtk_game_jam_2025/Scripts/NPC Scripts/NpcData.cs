
using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class NpcData : Node3D
{
    [ExportCategory("Npc Game Variables")]

    [Export] public TaskResource[] taskList;
    [Export] public String name;

    [ExportCategory("Animation Variables")]
    [Export] public Dictionary animations = new Dictionary();
    [Export] public AnimationPlayer playback;

    [ExportCategory("Movement Variables")]
    [Export] public float walkSpeed = 0.8f;
    [Export] public CharacterBody3D body3D;
}