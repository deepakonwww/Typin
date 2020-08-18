﻿namespace Typin.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public partial class RoutingSpecs
    {
        [Fact]
        public async Task Default_command_is_executed_if_provided_arguments_do_not_match_any_named_command()
        {
            // Arrange
            await using var stdOut = new MemoryStream();
            var console = new VirtualConsole(output: stdOut);

            var application = new CliApplicationBuilder()
                .AddCommand(typeof(DefaultCommand))
                .AddCommand(typeof(ConcatCommand))
                .AddCommand(typeof(DivideCommand))
                .UseConsole(console)
                .Build();

            // Act
            var exitCode = await application.RunAsync(
                Array.Empty<string>(),
                new Dictionary<string, string>());

            var stdOutData = console.Output.Encoding.GetString(stdOut.ToArray()).TrimEnd();

            // Assert
            exitCode.Should().Be(0);
            stdOutData.Should().Be("Hello world!");
        }

        [Fact]
        public async Task Default_command_is_executed_if_provided_arguments_do_not_match_any_named_command_and_is_only_command()
        {
            // Arrange
            await using var stdOut = new MemoryStream();
            var console = new VirtualConsole(output: stdOut);

            var application = new CliApplicationBuilder()
                .AddCommand(typeof(DefaultCommand))
                .UseConsole(console)
                .Build();

            // Act
            var exitCode = await application.RunAsync(
                Array.Empty<string>(),
                new Dictionary<string, string>());

            var stdOutData = console.Output.Encoding.GetString(stdOut.ToArray()).TrimEnd();

            // Assert
            exitCode.Should().Be(0);
            stdOutData.Should().Be("Hello world!");
        }

        [Fact]
        public async Task Default_benchmark_command_is_executed_if_provided_arguments_do_not_match_any_named_command_and_is_only_command()
        {
            string[] arguments = { "--str", "hello world", "-i", "13", "-b" };

            // Arrange
            await using var stdOut = new MemoryStream();
            var console = new VirtualConsole(output: stdOut);

            var application = new CliApplicationBuilder()
                .AddCommand(typeof(BenchmarkTypinCommand))
                .UseConsole(console)
                .Build();

            // Act
            var exitCode = await application.RunAsync(
                arguments,
                new Dictionary<string, string>());

            var stdOutData = console.Output.Encoding.GetString(stdOut.ToArray()).TrimEnd();

            // Assert
            exitCode.Should().Be(0);
            stdOutData.Should().BeEmpty();
        }

        [Fact]
        public async Task Help_text_is_printed_if_no_arguments_were_provided_and_default_command_is_not_defined()
        {
            // Arrange
            await using var stdOut = new MemoryStream();
            var console = new VirtualConsole(output: stdOut);

            var application = new CliApplicationBuilder()
                .AddCommand(typeof(ConcatCommand))
                .AddCommand(typeof(DivideCommand))
                .UseConsole(console)
                .UseDescription("This will be visible in help")
                .Build();

            // Act
            var exitCode = await application.RunAsync(
                Array.Empty<string>(),
                new Dictionary<string, string>());

            var stdOutData = console.Output.Encoding.GetString(stdOut.ToArray()).TrimEnd();

            // Assert
            exitCode.Should().Be(0);
            stdOutData.Should().Contain("This will be visible in help");
        }

        [Fact]
        public async Task Specific_named_command_is_executed_if_provided_arguments_match_its_name()
        {
            // Arrange
            await using var stdOut = new MemoryStream();
            var console = new VirtualConsole(output: stdOut);

            var application = new CliApplicationBuilder()
                .AddCommand(typeof(DefaultCommand))
                .AddCommand(typeof(ConcatCommand))
                .AddCommand(typeof(DivideCommand))
                .UseConsole(console)
                .Build();

            // Act
            var exitCode = await application.RunAsync(
                new[] { "concat", "-i", "foo", "bar", "-s", ", " },
                new Dictionary<string, string>());

            var stdOutData = console.Output.Encoding.GetString(stdOut.ToArray()).TrimEnd();

            // Assert
            exitCode.Should().Be(0);
            stdOutData.Should().Be("foo, bar");
        }
    }
}