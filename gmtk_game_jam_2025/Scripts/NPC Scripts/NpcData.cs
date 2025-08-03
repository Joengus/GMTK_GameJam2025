
using System;
using System.Collections.Generic;
using Godot;

public partial class NpcData : Node3D
{
    [ExportCategory("Npc Game Variables")]

    [Export] public TaskResource[] taskList;
    [Export] public String name;
    [Export] public String openingLine;

    [ExportCategory("Animation Variables")]
    [Export] string playbackPath;
    [Export] AnimationTree anim;
    AnimationNodeStateMachinePlayback playback => (AnimationNodeStateMachinePlayback)anim.Get(playbackPath);

    [ExportCategory("Movement Variables")]
    [Export] public float walkSpeed = 0.8f;
    [Export] public CharacterBody3D body3D;
    [Export] private float jumpHeight = 4.5f;
    [Export] public float jumpTime = 0.3f;
    private float gravity => 2 * jumpHeight / (jumpTime * jumpTime);
    private float jumpSpeed => Mathf.Sqrt(2 * jumpHeight * gravity);
}