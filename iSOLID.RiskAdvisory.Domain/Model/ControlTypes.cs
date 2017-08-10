using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public static class ControlTypes
    {
        private const string TEXT_BOX = "Text Box";
        private const string DATE_CONTROL = "Date Picker";
        private const string INTEGER = "Integer";
        private const string DECIMAL = "Decimal";

        public static string TextBox { get { return TEXT_BOX; } }

        public static string Integer { get { return INTEGER; } }

        public static string DateControl { get { return DATE_CONTROL; } }

        public static string Decimal { get { return DECIMAL; } }

    }
}
