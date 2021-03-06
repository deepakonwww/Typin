﻿namespace Typin.Directives
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Typin.Attributes;
    using Typin.Console;

    /// <summary>
    /// If application runs in interactive mode, this [.] directive can be used to remove one command from the scope.
    /// <example>
    ///             > [>] cmd1 sub
    ///     cmd1 sub> list
    ///     cmd1 sub> [.]
    ///         cmd1>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Directive(BuiltInDirectives.ScopeUp, Description = "Removes one command from the scope.", InteractiveModeOnly = true)]
    public sealed class ScopeUpDirective : IDirective
    {
        private readonly CliContext _cliContext;

        /// <inheritdoc/>
        public bool ContinueExecution => false;

        /// <summary>
        /// Initializes an instance of <see cref="ScopeUpDirective"/>.
        /// </summary>
        public ScopeUpDirective(ICliContext cliContext)
        {
            _cliContext = (CliContext)cliContext;
        }

        /// <inheritdoc/>
        public ValueTask HandleAsync(IConsole console)
        {
            // Scope up
            if (_cliContext.Input.HasDirective(BuiltInDirectives.ScopeUp))
            {
                string[] splittedScope = _cliContext.Scope.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (splittedScope.Length > 1)
                    _cliContext.Scope = string.Join(" ", splittedScope, 0, splittedScope.Length - 1);
                else if (splittedScope.Length == 1)
                    _cliContext.Scope = string.Empty;
            }

            return default;
        }
    }
}
