using Godot;
using System;

public partial class Player : CharacterBody3D
{
    //You can set this so that you require these nodes to be built for player and characters.
    [ExportGroup("Required Nodes")]
    //We're making it publicly available to attach an AnimationPlayer node to this Player node.
    [Export] private AnimationPlayer animationPlayerNode;
    //We're making it publicly availably to attach a Sprite3D node to this Player node.
    [Export] private Sprite3D spriteNode;

    //Instancing a new version of Vector2 to convert to Vector3. We wouldn't have to do this in a 2D only game.
    private Vector2 direction = new();

    public override void _Ready()
    {
        animationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        //Setting the Velocity property to a new instance of Vector2 converted to Vector3.We wouldn't have to do this in a 2D only game. 
        Velocity = new(direction.X, 0, direction.Y); //This remaps Z to known Y and leaves Y blank because in 3D space for Godot that's up into the air or down in the ground. We don't want that right now.
        //This is so that the default speed isn't incredibly slow.
        Velocity *= 5;
        //Enables velocity to move with the physics engine. You don't need to specify delta with MoveAndSlide.
        MoveAndSlide();
    }

    //Establish some controls.
    public override void _Input(InputEvent @event)
    {
        //Input.GetVector() takes up to four parameters and gives us an easy way of moving in all 8 directions just by listing the input names in quotes.
       direction = Input.GetVector(
            "MoveLeft",
            "MoveRight",
            "MoveUp",
            "MoveDown"
        );

        if(direction == Vector2.Zero)
        {
            animationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
        }
        else
        {
            animationPlayerNode.Play(GameConstants.ANIMATION_MOVE);
        }
    }

}
