﻿namespace Typin.Tests.Data.Commands.Valid
{
    using Typin.Attributes;
    using Typin.Tests.Data.Commands;
    using Typin.Tests.Data.CustomTypes.NonInitializable;
    using Typin.Tests.Data.Valid;

    [Command("cmd")]
    public class UnsupportedArgumentTypesCommand : SelfSerializeCommandBase
    {
        [CommandOption("str-non-initializable")]
        public NonInitializableClassType? StringNonInitializable { get; set; }

        [CommandOption("str-non-initializable")]
        public NonInitializableStructType? StringNonInitializableStruct { get; set; }

        [CommandOption("str-enumerable-non-initializable")]
        public NonInitializableEnumerable<string>? StringEnumerableNonInitializable { get; set; }
    }
}