using System.Collections.Generic;

namespace Command
{
    public class ModifyPrice
    {
        private readonly List<ICommand> commands;
        private ICommand command;

        public ModifyPrice()
        {
            this.commands = new List<ICommand>();
        }

        public void SetCommand(ICommand command)
            => this.command = command;

        public void Invoke()
        {
            this.commands.Add(command);
            this.command.ExecuteAction(); ;
        }
    }
}