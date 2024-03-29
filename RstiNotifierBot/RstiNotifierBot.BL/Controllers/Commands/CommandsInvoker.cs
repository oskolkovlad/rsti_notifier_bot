﻿namespace RstiNotifierBot.BL.Controllers.Commands
{
    using System;
    using System.Collections.Generic;
    using RstiNotifierBot.BL.Constants;
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;
    using RstiNotifierBot.BL.Interfaces.Commands;

    internal class CommandsInvoker : ICommandsInvoker
    {
        private readonly IDictionary<string, ICommand> _commands;

        public CommandsInvoker(params ICommand[] commands)
        {
            _commands = new Dictionary<string, ICommand>();

            foreach (var command in commands)
            {
                _commands.Add(command.Type, command);
            }
        }

        #region ICommandsInvoker Members

        public CommandResult Execute(CommandContext context, string command)
        {
            CommandResult result;

            switch (command)
            {
                case Commands.Start:
                case Commands.Last:
                case Commands.Top5:
                case Commands.Subscribe:
                case Commands.Unsubscribe:
                case Commands.Info:
                    try
                    {
                        result = _commands[command].Execute(context);
                    }
                    catch(Exception exception)
                    {
                        throw;
                    }
                    break;

                default:
                    result = new CommandResult(false);
                    break;
            }

            return result;
        }

        #endregion
    }
}
