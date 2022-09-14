using System;
using System.Text;

namespace Test
{
    internal class Position
    {
        private string VendorCode { get; set; }
        private string Name { get; set; }
        private double? Amount { get; set; }
        private string Units { get; set; }
        private string Comments { get; set; }
        private string Date { get; set; }
        private string From { get; set; }

        private Position () 
        {
        }
        public static Position Default (string vendorCode, string name, double? amount, string date, string from)
        {
            Position position = new Position ();
            position.VendorCode = vendorCode;
            position.Name = name;
            position.Amount = amount;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position WhithUnits (string vendorCode, string name, double? amount, string units, string date, string from)
        {
            Position position = new Position();
            position.VendorCode = vendorCode;
            position.Name= name;
            position.Amount = amount;
            position.Units = units;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position WithComments (string vendorCode, string name, double? amount, string comments, string date, string from)
        {
            Position position = new Position();
            position.VendorCode = vendorCode;
            position.Name = name;
            position.Amount = amount;
            position.Comments = comments;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position WithUnitsAndComments (string vendorCode, string name, double? amount, string units, string comments, string date, string from)
        {
            Position position = new Position();
            position.VendorCode = vendorCode;
            position.Name = name;
            position.Amount = amount;
            position.Units = units;
            position.Comments = comments;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position Error (string name, string date, string from)
        {
            Position position = new Position();
            position.Name = name;
            position.Date = date;
            position.From = from;
            return position;
        }
        public override string ToString()
        {
            return $"Статья: {VendorCode}" + $"   Позиция: {Name}" + $"   Колличество: {Amount}" + $"   Ед. Измерения: {Units}" + $"   Комментарий: {Comments}" + $"   Дата: {Date}" + $"   От: {From}";
        }
    } 
}
