using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk.Exceptions
{
    public class ChalkException:Exception
    {
        public ChalkException(string message):base()
        {
#if UNITY_EDITOR
            Debug.LogError(message + "Source: " + Source);
#endif
        }
    }
    public class ChalkMissingValueException : ChalkException 
    {
        public ChalkMissingValueException(string valueName):base("Missing " + valueName+".") { }
    }
    public class ChalkWrongNumberValueException : ChalkException
    {
        public enum ZeroComparasionRequirement
        {
            Greater,
            GreaterOrEqual,
            Less,
            LessOrEqual,
            NotEqual
        }

        private static string GetRequirementDescription(ZeroComparasionRequirement requirement)
        {
            switch (requirement)
            {
                case ZeroComparasionRequirement.Greater: return "greater than";
                case ZeroComparasionRequirement.GreaterOrEqual: return "greater or equal";
                case ZeroComparasionRequirement.Less: return "less than";
                case ZeroComparasionRequirement.LessOrEqual: return "less or equal";
                case ZeroComparasionRequirement.NotEqual: return "not equal";

                default: return "";
            }
        }
        public ChalkWrongNumberValueException(string valueName,ZeroComparasionRequirement requirement) :
            base($"{valueName} {GetRequirementDescription(requirement)} zero.") { }
    }
    public class ChalkParsingException : ChalkException
    {
        public ChalkParsingException(string valueName):
            base("Cant parse " + valueName + ".")
        { }
    }
    public class ChalkArrayIndexOutOfRangeException : ChalkException
    {
        public ChalkArrayIndexOutOfRangeException(string arrayName,int index,int arrayLength):
            base($"Index {index} is out of range of array {arrayName}(length: {arrayLength}).")
        { }
    }
}
