﻿namespace Typin.Tests.Data.Commands.Valid
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Typin.Attributes;
    using Typin.Tests.Data.Commands;

    [Command("cmd")]
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
    public class WithDefaultValuesCommand : SelfSerializeCommandBase
    {
        public enum CustomEnum { Value1, Value2, Value3 };

        [CommandOption("obj")]
        public object? Object { get; set; } = 42;

        [CommandOption("str")]
        public string? String { get; set; } = "foo";

        [CommandOption("str-empty")]
        public string StringEmpty { get; set; } = "";

        [CommandOption("str-array")]
        public string[]? StringArray { get; set; } = { "foo", "bar", "baz" };

        [CommandOption("bool")]
        public bool Bool { get; set; } = true;

        [CommandOption("char")]
        public char Char { get; set; } = 't';

        [CommandOption("int")]
        public int Int { get; set; } = 1337;

        [CommandOption("int-nullable")]
        public int? IntNullable { get; set; } = 1337;

        [CommandOption("int-array")]
        public int[]? IntArray { get; set; } = { 1, 2, 3 };

        [CommandOption("timespan")]
        public TimeSpan TimeSpan { get; set; } = TimeSpan.FromMinutes(123);

        [CommandOption("enum")]
        public CustomEnum Enum { get; set; } = CustomEnum.Value2;
    }
}