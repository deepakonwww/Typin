﻿namespace Typin.Tests.Data.CustomTypes.Initializable
{
    public class CustomStringConstructible
    {
        public string Value { get; }

        public CustomStringConstructible(string value)
        {
            Value = value;
        }
    }
}