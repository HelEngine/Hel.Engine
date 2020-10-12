using Hel.Engine.Commander.Model.Payload;

namespace Hel.Engine.Commander.Model
{
    /// <summary>
    /// Interface for all commands to use. 
    /// Commands provide you with a way to map button presses to commands
    /// Example: BUTTON_SPACE -> JumpCommand
    /// BUTTON_A -> MoveUnitCommand
    /// An instance of these commands is created each time its required and is destroyed after the logic is called.
    /// Commands can either have their own logic, or simply call something else. 
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute the command action.
        /// </summary>
        abstract void Execute();

        /// <summary>
        /// Undo the last performed command action
        /// </summary>
        abstract void Undo();
    }

    /// <summary>
    /// ICommand that implements a payload
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommand<T> : ICommand where T: ICommandPayload
    {
        /// <summary>
        /// Payload used by the command
        /// </summary>
        T Payload { get; set; } 
    }
}
