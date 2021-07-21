using System;

namespace Orion.Manager.Core.Common.ValueObjects
{
    public class TwoFactorCode
    {
        public string Value { get; }
        
        public TwoFactorCode()
        {
            Value = new Random().Next(0, 1000000).ToString("D6");
        }
    }
}